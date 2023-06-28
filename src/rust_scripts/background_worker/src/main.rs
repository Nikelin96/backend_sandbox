mod structures;
use structures::Scenario;
use tokio::sync::mpsc;
mod steps_monitor;

#[tokio::main]
async fn main() {
    let (sender, mut receiver) = mpsc::channel::<Scenario>(10);

    let sender_second = sender.clone();
    let sender_third = sender.clone();

    tokio::spawn(async move {
        let file_path = "example_scenarios.json";
        if let Err(e) = steps_monitor::read_scenarios_from_file_async(file_path, sender).await {
            eprintln!("Error reading scenarios from file: {}", e);
        }
    });

    tokio::spawn(async move {
        if let Err(e) = steps_monitor::read_scenarios_from_memory_async(sender_second).await {
            eprintln!("Error reading scenarios from memory: {}", e);
        }
    });

    tokio::spawn(async move {
        let second_file_path = "example_scenarios2.json";
        if let Err(e) =
            steps_monitor::read_scenarios_from_file_async(second_file_path, sender_third).await
        {
            eprintln!("Error reading scenarios from file: {}", e);
        }
    });

    // Process the received scenarios
    while let Some(scenario) = receiver.recv().await {
        let processor = steps_monitor::get_scenario_processor(&scenario.system_type);
        match processor.handle_step(&scenario) {
            Ok(_) => println!("Successfully processed scenario: {}", scenario),
            Err(e) => eprintln!("Error processing scenario: {}\nError: {}", scenario, e),
        }
    }
}
