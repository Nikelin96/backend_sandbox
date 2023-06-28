use serde::{Deserialize, Serialize};
use std::{
    collections::HashMap,
    fmt::{self, Display, Formatter},
};
use uuid::Uuid;

#[derive(Serialize, Deserialize, Debug, Clone)]
pub enum SystemType {
    Crm,
    Sql,
    GraphQL,
}

impl Display for SystemType {
    fn fmt(&self, f: &mut Formatter) -> fmt::Result {
        let display_string = match self {
            SystemType::Crm => "Crm",
            SystemType::Sql => "Sql",
            SystemType::GraphQL => "GraphQL",
        };
        write!(f, "{}", display_string)
    }
}

#[derive(Serialize, Deserialize, Debug, Clone)]
pub struct Scenario {
    #[serde(default = "generate_uuid")]
    pub id: Uuid,
    pub system_type: SystemType,
    pub step_type: String,
    pub step_name: String,
    #[serde(default, skip_serializing_if = "Option::is_none")]
    pub payload: Option<serde_json::Value>,
    #[serde(default)]
    pub variables_to_capture: Option<HashMap<String, String>>,
}

impl Scenario {
    pub fn new(
        id: Uuid,
        system_type: SystemType,
        step_type: String,
        step_name: String,
        payload: Option<serde_json::Value>,
        variables_to_capture: Option<HashMap<String, String>>,
    ) -> Self {
        Self {
            id,
            system_type,
            step_type,
            step_name,
            payload,
            variables_to_capture,
        }
    }
}

fn generate_uuid() -> Uuid {
    Uuid::new_v4()
}

impl Display for Scenario {
    fn fmt(&self, f: &mut Formatter) -> fmt::Result {
        let system_type = format!("{}", self.system_type);
        let step_type = &self.step_type;
        let step_name = &self.step_name;

        let payload_str = match &self.payload {
            Some(payload) => format!("{}", payload),
            None => "None".to_string(),
        };

        let variables_to_capture_str = match &self.variables_to_capture {
            Some(variables) => {
                let variables_str: Vec<String> = variables
                    .iter()
                    .map(|(key, value)| format!("{}: {}", key, value))
                    .collect();
                format!("{{{}}}", variables_str.join(", "))
            }
            None => "None".to_string(),
        };

        write!(
            f,
            "\nScenario {{ id: {}, system_type: {}, step_type: {}, step_name: {} }}",
            self.id, system_type, step_type, step_name
        )

        // write!(
        //     f,
        //     "Scenario {{ id: {}, system_type: {}, step_type: {}, step_name: {}, payload: {}, variables_to_capture: {} }}",
        //     self.id, system_type, step_type, step_name, payload_str, variables_to_capture_str
        // )
    }
}

pub trait ScenarioProcessor {
    fn handle_step(&self, scenario: &Scenario) -> Result<(), String>;
}

pub struct CrmProcessor;
pub struct SqlProcessor;
pub struct GraphQLProcessor;

impl ScenarioProcessor for CrmProcessor {
    fn handle_step(&self, scenario: &Scenario) -> Result<(), String> {
        println!("Handling CRM scenario: {}", scenario);
        // Add your CRM processing logic here
        Ok(())
    }
}

impl ScenarioProcessor for SqlProcessor {
    fn handle_step(&self, scenario: &Scenario) -> Result<(), String> {
        println!("Handling SQL scenario: {}", scenario);
        // Add your SQL processing logic here
        Ok(())
    }
}

impl ScenarioProcessor for GraphQLProcessor {
    fn handle_step(&self, scenario: &Scenario) -> Result<(), String> {
        println!("Handling GraphQL scenario: {}", scenario);
        // Add your GraphQL processing logic here
        Ok(())
    }
}
