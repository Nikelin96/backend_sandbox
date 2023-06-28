use std::collections::HashMap;
use tokio::fs::File;
use tokio::io::{self, AsyncReadExt};
use tokio::sync::mpsc::Sender;
use tokio::time::{sleep, Duration};
use uuid::Uuid;

use crate::structures::{
    CrmProcessor, GraphQLProcessor, Scenario, ScenarioProcessor, SqlProcessor, SystemType,
};

pub async fn read_scenarios_from_file_async(
    file_path: &str,
    sender: Sender<Scenario>,
) -> io::Result<()> {
    // Read the contents of the file asynchronously
    let mut file = File::open(file_path).await?;
    let mut contents = String::new();
    file.read_to_string(&mut contents).await?;

    // Parse the JSON content into a Vec<Scenario>
    let scenarios: Vec<Scenario> = match serde_json::from_str(&contents) {
        Ok(scenarios) => scenarios,
        Err(e) => {
            eprintln!("Error parsing JSON: {}", e);
            return Err(io::Error::new(io::ErrorKind::InvalidData, e));
        }
    };

    let source_id = Uuid::new_v4();

    for mut scenario in scenarios {
        scenario.id = source_id;
        if let Err(e) = sender.send(scenario).await {
            eprintln!("Error sending scenario: {}", e);
            return Err(io::Error::new(io::ErrorKind::BrokenPipe, e));
        }
        sleep(Duration::from_secs(1)).await;
    }

    Ok(())
}

pub async fn read_scenarios_from_memory_async(sender: Sender<Scenario>) -> io::Result<()> {
    let source_id = Uuid::new_v4();

    let vals = vec![
        Scenario::new(
            source_id,
            SystemType::Sql,
            String::from("ExampleStep"),
            String::from("Initial Sql Step"),
            None,
            Some({
                let mut map = HashMap::new();
                map.insert(String::from("shipments"), String::from("$.login.sdgs..id"));
                map.insert(
                    String::from("shipmentNames"),
                    String::from("$.login.sdgs..name"),
                );
                map
            }),
        ),
        Scenario::new(
            source_id,
            SystemType::GraphQL,
            String::from("InitialStep"),
            String::from("Initial Json Step"),
            None,
            None,
        ),
        Scenario::new(
            source_id,
            SystemType::Crm,
            String::from("InitialStep"),
            String::from("Initial Crm Step"),
            None,
            None,
        ),
        Scenario::new(
            source_id,
            SystemType::Crm,
            String::from("UnknownStep"),
            String::from("Initial Unknown Step"),
            None,
            None,
        ),
    ];

    for value in vals {
        if let Err(e) = sender.send(value).await {
            eprintln!("Error sending scenario: {}", e);
            return Err(io::Error::new(io::ErrorKind::BrokenPipe, e));
        }
        sleep(Duration::from_secs(1)).await;
    }

    Ok(())
}

pub fn get_scenario_processor(system_type: &SystemType) -> Box<dyn ScenarioProcessor> {
    match system_type {
        SystemType::Crm => Box::new(CrmProcessor),
        SystemType::Sql => Box::new(SqlProcessor),
        SystemType::GraphQL => Box::new(GraphQLProcessor),
    }
}
