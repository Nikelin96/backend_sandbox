BEGIN;

-- Inserting Continent 1
INSERT INTO continent (name) VALUES ('Europe');

-- Inserting Kingdom(England) which is part of Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('England', 1, 1);

-- Inserting costs for blueprints
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 10, 5, 15, 0); -- Spearmen Blueprint
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 5, 2, 10, 0);  -- Spear Blueprint
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 3, 2, 8, 0);   -- Attack Skill Blueprint
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 4, 2, 9, 0);   -- Defend Skill Blueprint
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 6, 3, 12, 0);  -- Mail Armor Blueprint

-- Inserting records for the blueprints
INSERT INTO blueprint (name, description, research_time, storage_id) VALUES ('Spearmen Blueprint', 'A blueprint for training a Spearmen', 50, 1);
INSERT INTO blueprint (name, description, research_time, storage_id) VALUES ('Spear Blueprint', 'A blueprint for crafting a Spear', 30, 2);
INSERT INTO blueprint (name, description, research_time, storage_id) VALUES ('Attack Skill Blueprint', 'A blueprint for learning the Attack skill', 20, 3);
INSERT INTO blueprint (name, description, research_time, storage_id) VALUES ('Defend Skill Blueprint', 'A blueprint for learning the Defend skill', 25, 4);
INSERT INTO blueprint (name, description, research_time, storage_id) VALUES ('Mail Armor Blueprint', 'A blueprint for crafting a Mail Armor', 40, 5);

-- Inserting records for the blueprint dependencies
INSERT INTO blueprint_dependency (blueprint_id, required_blueprint_id, is_optional) VALUES (1, 2, FALSE);
INSERT INTO blueprint_dependency (blueprint_id, required_blueprint_id, is_optional) VALUES (1, 3, FALSE);
INSERT INTO blueprint_dependency (blueprint_id, required_blueprint_id, is_optional) VALUES (1, 4, FALSE);
INSERT INTO blueprint_dependency (blueprint_id, required_blueprint_id, is_optional) VALUES (1, 5, TRUE);

-- Inserting records for unit -> 'Spearmen'
INSERT INTO stat (hit_points, damage_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);

INSERT INTO storage (type, wood, food, gold) VALUES ('price', 3, 2, 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 6);

-- Inserting records for equipment: 'Spearmen' -> 'Spear'
INSERT INTO stat (hit_points, damage_points) VALUES (0, 10);
INSERT INTO equipment (name, stat_id, unit_id) VALUES ('Spear', 2, 1);

INSERT INTO storage (type, wood, gold) VALUES ('price',  2, 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 7);

-- Inserting records for skill: 'Spearmen' -> 'Attack'
INSERT INTO stat (hit_points, damage_points) VALUES (0, 10);
INSERT INTO skill (name, type, unit_id, stat_id) VALUES ('Attack', 'attack', 1, 3);

INSERT INTO storage (type, food) VALUES ('price', 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 8);

-- Inserting records for skill: 'Spearmen' -> 'Defend'
INSERT INTO stat (hit_points, defense_points) VALUES (10, 20);
INSERT INTO skill (name, type, unit_id, stat_id) VALUES ('Defend', 'defend', 1, 4);

INSERT INTO storage (type, food) VALUES ('price', 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 9);

-- Inserting records for unit -> 'Market'
INSERT INTO stat (hit_points, defense_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Market', 5, 1); 
INSERT INTO market (tax, unit_id) VALUES (30, 2);

INSERT INTO storage (type, wood, food, gold, stone) VALUES ('price', 100, 100, 100, 100); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (2, 10);

-- Inserting records for equipment: 'Market' -> 'Mail armor'
INSERT INTO stat (hit_points, defense_points) VALUES (10, 20);
INSERT INTO equipment (name, stat_id, unit_id) VALUES ('Mail armor', 6, 2);

INSERT INTO storage (type, gold) VALUES ('price', 50); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (2, 11); 

-- Inserting transaction: 'Kingdom' -> 'income'
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('income', 200, 200, 200, 200);
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 12);

-- Inserting transaction: Kingdom -> 'expense'(for Spearmen with his required skills(attack&defend) and equipment(spear))
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('expense', 5, 4, 2, 0); 
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 13);

-- Inserting transaction: Kingdom -> 'expense'(for Market with Mail armor)
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('expense', 100, 100, 150, 100); 
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 14);

-- Inserting records for kingdom_blueprints
-- Purchasing 'Spear Blueprint'
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 2);
INSERT INTO kingdom_blueprint (kingdom_id, storage_id, blueprint_id, research_status, research_start_time)
VALUES (1, 2, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');

-- Purchasing 'Attack Skill Blueprint'
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 3);
INSERT INTO kingdom_blueprint (kingdom_id, storage_id, blueprint_id, research_status, research_start_time)
VALUES (1, 3, 3, 'completed', CURRENT_TIMESTAMP - INTERVAL '20 days');

-- Purchasing 'Defend Skill Blueprint'
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 4);
INSERT INTO kingdom_blueprint (kingdom_id, storage_id, blueprint_id, research_status, research_start_time)
VALUES (1, 4, 4, 'completed', CURRENT_TIMESTAMP - INTERVAL '25 days');

-- Purchasing 'Mail Armor Blueprint'
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 5);
INSERT INTO kingdom_blueprint (kingdom_id, storage_id, blueprint_id, research_status, research_start_time)
VALUES (1, 5, 5, 'completed', CURRENT_TIMESTAMP - INTERVAL '40 days');

-- Finally, purchasing 'Spearmen Blueprint'
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 1);
INSERT INTO kingdom_blueprint (kingdom_id, storage_id, blueprint_id, research_status, research_start_time)
VALUES (1, 1, 1, 'not started', CURRENT_TIMESTAMP);

COMMIT;