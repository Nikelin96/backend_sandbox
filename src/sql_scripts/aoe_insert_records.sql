BEGIN;


-- Insert Continent(Europe)
INSERT INTO continent (name) VALUES ('Europe');


-- Insert Kingdom(England) --> Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('England', 1, 1);
-- Insert Kingdom(France) --> Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('France', 1, 1);


-- Insert transaction: 'Kingdom' -> 'income'
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'income', 200, 200, 200, 200);


-- Insert Technology(Unit -> Spearmen)
INSERT INTO technology (name, description, research_time) VALUES ('Spearmen technology', 'A technology for training a Spearmen', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (10, 10, 10, 10, 1, null);
-- Insert Unit(Spearmen)
INSERT INTO stat (hit_points, damage_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id) VALUES ('Spearmen', 1);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (5, 5, 5, 5, null, 1);
-- Insert technology dependencies: Technology(Spearmen) --> Unit(Spearmen)
INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id) VALUES(1, true, 1, null);


-- Insert technology(Skill -> attack)
INSERT INTO technology (name, description, research_time) VALUES ('attack technology', 'A technology for attacking', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (10, 10, 10, 10, 2, null, null, null);
-- Insert Skill(attack)
INSERT INTO stat (hit_points) VALUES (10);
INSERT INTO skill (type, stat_id) VALUES ('attack', 2); 
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (4, 4, 4, 4, null, null, 1, null);
-- Insert technology dependencies: Technology(attak) --> Skill(attack)
INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id) VALUES(2, true, null, 1);


-- Insert technology(Equipment -> chain mail)
INSERT INTO technology (name, description, research_time) VALUES ('chain mail', 'A technology for chain mail', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (10, 10, 10, 10, 3, null, null, null);
-- Insert Equipment(chain mail)
INSERT INTO stat (health_points, defense_points) VALUES (10, 2);
INSERT INTO equipment (name, stat_id) VALUES ('chain mail', 3); 
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (3, 3, 3, 3, null, null, null, 1);
-- Insert technology dependencies: Technology(attak) --> Skill(attack)
INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id, equipment_id) VALUES(3, true, null, null, 1);


-- Insert transaction: Kingdom -> expense -> Technology(Unit -> Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 10, 10, 10, 10, 1, null, null, null);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');


-- Insert transaction: Kingdom -> expense -> Technology(Skill -> attack)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 10, 10, 10, 10, 2, null, null, null);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 2, 3, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');


-- Insert transaction: Kingdom -> expense -> Technology(Equipment -> chain mail)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 10, 10, 10, 10, 3, null, null, null);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 2, 4, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');


-- Insert transaction: Kingdom -> expense -> Unit(Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 5, 5, 5, 5, null, 1, null, null);
-- Insert transaction: Kingdom -> expense -> Skill(attack)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 4, 4, 4, 4, null, null, 1, null);
-- Insert transaction: Kingdom -> expense -> Equipment(chain mail)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone, technology_id, unit_id, skill_id, equipment_id) VALUES (1, 'expense', 3, 3, 3, 3, null, null, null, 1);


-- Attach Unit(Spearmen) --> Kingdom(England)
INSERT INTO kingdom_unit (kingdom_id, unit_id, kingdom_transaction_id) VALUES (1, 1, 4);
-- Attach Skill(attack) --> Unit(Spearmen) --> Kingdom(England)
INSERT INTO unit_skill (skill_id, unit_id) VALUES (1, 1);
-- Attach Equipment(chain mail) --> Unit(Spearmen) --> Kingdom(England)
INSERT INTO unit_equipment (equipment_id, unit_id) VALUES (1, 1);


COMMIT;