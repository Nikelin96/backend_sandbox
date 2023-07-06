BEGIN;

-- Inserting Continent 1
INSERT INTO continent (name) VALUES ('Continent 1');

-- Inserting Kingdom 1 which is part of Continent 1
INSERT INTO kingdom (name, rank, continent_id) VALUES ('Kingdom 1', 1, 1);

-- Inserting records for Spearmen 'unit'
INSERT INTO stat (hit_points, damage_points) VALUES (100, 1);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Spearmen', 1, 1);

INSERT INTO storage (type, food, gold, wood) VALUES ('expense', 2, 1, 3); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 1);

-- Inserting records for Spearmen equipment 'spear'
INSERT INTO stat (hit_points, damage_points) VALUES (0, 10);
INSERT INTO equipment (name, stat_id, unit_id) VALUES ('Spear', 2, 1);

INSERT INTO storage (type, gold, wood) VALUES ('expense', 1, 2); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 2);

-- Inserting records for Spearmen skill 'attack'
INSERT INTO stat (hit_points, damage_points) VALUES (0, 10);
INSERT INTO skill (name, type, unit_id, stat_id) VALUES ('Attack', 'attack', 1, 3);

INSERT INTO storage (type, food) VALUES ('expense', 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 3);

-- Inserting records for Spearmen skill 'defend'
INSERT INTO stat (hit_points, defense_points) VALUES (10, 2);
INSERT INTO skill (name, type, unit_id, stat_id) VALUES ('Defend', 'defend', 1, 4);

INSERT INTO storage (type, food) VALUES ('expense', 1); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (1, 4);

-- Inserting records for unit 'Market'
INSERT INTO stat (hit_points, defense_points) VALUES (100, 1);
INSERT INTO unit (name, stat_id, kingdom_id) VALUES ('Market', 5, 1); 
INSERT INTO market (tax, unit_id) VALUES (30, 2);

INSERT INTO storage (type, wood, food, gold, stone) VALUES ('expense', 100, 100, 100, 100); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (2, 5);

-- Inserting records for Market equipment 'Mail armor'
INSERT INTO stat (hit_points, defense_points) VALUES (10, 2);
INSERT INTO equipment (name, stat_id, unit_id) VALUES ('Mail armor', 6, 2);

INSERT INTO storage (type, gold) VALUES ('expense', 50); 
INSERT INTO unit_storage_transactions (unit_id, storage_id) VALUES (2, 6); 

-- Inserting Kingdom transaction 'income'
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('income', 200, 200, 200, 200);
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 7);

-- Inserting Kingdom transaction 'expense'
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('expense', 5, 4, 2, 0); 
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 8);

-- Inserting Kingdom transaction 'expense'
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('expense', 100, 100, 150, 100); 
INSERT INTO kingdom_storage_transactions (kingdom_id, storage_id) VALUES (1, 9);

COMMIT;
