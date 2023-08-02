BEGIN;

-- Insert Continent(Europe)
INSERT INTO continent (name) VALUES ('Europe');

-- Insert Kingdom(England) --> Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('England', 1, 1);
-- Insert transaction: 'Kingdom' -> 'income'
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'income', 200, 200, 200, 200);

-- Insert Technology(Unit -> Spearmen)
INSERT INTO technology (name, description, research_time) VALUES ('Spearmen technology', 'A technology for training a Spearmen', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (10, 10, 10, 10, 1, null);

-- Insert Unit(Spearmen)
INSERT INTO stat (hit_points, damage_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (5, 5, 5, 5, null, 1);
-- Insert technology dependencies for Unit(Spearmen)
INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id) VALUES(1, true, 1, null);

-- Insert transaction: Kingdom -> expense -> Technology(Unit -> Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 10, 10, 10, 10);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');

-- Insert transaction: Kingdom -> expense -> Unit(Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 5, 5, 5, 5);
INSERT INTO kingdom_unit (kingdom_id, unit_id, kingdom_transaction_id) VALUES (1, 1, 3);

-- Insert technology(Skill -> attack)
INSERT INTO technology (name, description, research_time) VALUES ('attack technology', 'A technology for attacking', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id) VALUES (10, 10, 10, 10, 2, null, null);

INSERT INTO stat (hit_points) VALUES (10);
INSERT INTO skill (type, stat_id) VALUES ('attack', 2); 
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id, skill_id) VALUES (5, 5, 5, 5, null, null, 1);
-- Insert technology dependencies for Skill(attack)
INSERT INTO technology_dependency (technology_id, is_required, unit_id, skill_id) VALUES(2, true, null, 1);

-- Insert transaction: Kingdom -> expense -> Technology(Skill -> attack)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 10, 10, 10, 10);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 2, 4, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');

-- Insert transaction: Kingdom -> expense -> Skill(Attack)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 4, 4, 4, 4);
-- Attach Skill(attack) --> Unit(Spearmen) --> Kingdom(England)
INSERT INTO unit_skill (skill_id, unit_id, kingdom_transaction_id) VALUES (1, 1, 5);

COMMIT;