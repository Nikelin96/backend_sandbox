BEGIN;

-- Inserting Continent(Europe)
INSERT INTO continent (name) VALUES ('Europe');

-- Inserting Kingdom(England) which is part of Continent(Europe)
INSERT INTO kingdom (name, rank, continent_id) VALUES ('England', 1, 1);
-- Inserting transaction: 'Kingdom' -> 'income'
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'income', 200, 200, 200, 200);

-- Insert Technology(Spearmen)
INSERT INTO technology (name, description, research_time) VALUES ('Spearmen technology', 'A technology for training a Spearmen', 50);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (10, 10, 10, 10, 1, null);

-- Inserting Unit(Spearmen)
INSERT INTO stat (hit_points, damage_points) VALUES (100, 10);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);
INSERT INTO price(wood, food, gold, stone, technology_id, unit_id) VALUES (5, 5, 5, 5, null, 1);
-- Insert technology Dependencies for Unit(Spearmen)
INSERT INTO technology_dependency (technology_id, is_required, unit_id) VALUES(1, true, 1);

-- Inserting transaction: 'Kingdom' -> 'expense' -> 'Technology'(Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 10, 10, 10, 10);
INSERT INTO kingdom_technology (kingdom_id, technology_id, kingdom_transaction_id, research_status, research_start_time) VALUES(1, 1, 2, 'completed', CURRENT_TIMESTAMP - INTERVAL '30 days');

-- Inserting transaction: 'Kingdom' -> 'expense' -> 'Unit'(Spearmen)
INSERT INTO kingdom_transaction (kingdom_id, type, wood, food, gold, stone) VALUES (1, 'expense', 5, 5, 5, 5);
INSERT INTO kingdom_unit (kingdom_id, unit_id, kingdom_transaction_id) VALUES (1, 1, 3);

COMMIT;