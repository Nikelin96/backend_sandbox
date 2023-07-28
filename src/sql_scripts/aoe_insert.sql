BEGIN;

-- Inserting Continent(Europe)
INSERT INTO continent (name) VALUES ('Europe');

-- Inserting Kingdom(England) which is part of Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('England', 1, 1);
-- Inserting transaction: 'Kingdom' -> 'income'
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'income', 200, 200, 200, 200);

-- Insert Blueprint(Spearmen)
INSERT INTO blueprint (name, description, research_time) VALUES ('Spearmen Blueprint', 'A blueprint for training a Spearmen', 50);
INSERT INTO price(wood, food, gold, stone, blueprint_id) VALUES (10, 10, 10, 10, 1);

-- Inserting Unit(Spearmen)
INSERT INTO stat (hit_points, damage_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);
-- Insert Blueprint Dependencies for Unit(Spearmen)
INSERT INTO blueprint_dependency (blueprint_id, is_required, unit_id) VALUES(1, true, 1);

-- Inserting transaction: 'Kingdom' -> 'expense'
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 10, 10, 10, 10);
INSERT INTO kingdom_blueprint (kingdom_id, blueprint_id, transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');

COMMIT;