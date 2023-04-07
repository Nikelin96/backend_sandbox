
use std::time::Duration;
use tokio::fs;

#[tokio::main]
async fn main() {
    // Spawn a new task to read the JSON file every minute
    tokio::spawn(async move {
        loop {
            let file_contents = read_file().await.unwrap();
            println!("File contents: {}", file_contents);
            tokio::time::sleep(Duration::from_secs(60)).await;
        }
    });

    // Main execution thread accepts string input
    loop {
        let mut input = String::new();
        println!("Enter a string:");
        std::io::stdin().read_line(&mut input).unwrap();
        println!("You entered: {}", input);
    }
}

async fn read_file() -> Result<String, std::io::Error> {
    let file_contents = fs::read_to_string("example.json").await?;
    Ok(file_contents)
}
