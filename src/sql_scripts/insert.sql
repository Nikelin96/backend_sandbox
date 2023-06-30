BEGIN;

-- create Spearmen with Spear
INSERT INTO storage (type, food, gold, wood) VALUES ('expense', 5, 10, NULL); -- id 1 for spearmen
INSERT INTO stat (hit_points, damage_points) VALUES (100, 1); -- id 1 for spearmen
INSERT INTO unit (name, stat_id, storage_id) VALUES ('Spearmen', 1, 1); -- id 1

INSERT INTO storage (type, food, gold, wood) VALUES ('expense', NULL, NULL, 5); -- id 2 for spear
INSERT INTO stat (hit_points, damage_points) VALUES (0, 10); -- id 2 for spear
INSERT INTO equipment (name, stat_id, storage_id, unit_id) VALUES ('Spear', 2, 2, 1); -- id 1 for Spear, referencing unit id 1

-- create Market with Mail armor
INSERT INTO storage (type, wood, food, gold, stone) VALUES ('income', 100, 100, 100, 100); -- id 3 for market
INSERT INTO stat (hit_points, defense_points) VALUES (100, 1); -- id 3 for market
INSERT INTO unit (name, stat_id, storage_id) VALUES ('Market', 3, 3); -- id 2 for market

INSERT INTO market (tax, unit_id) VALUES (30, 2); -- referencing unit id 2 for market

INSERT INTO storage (type, gold) VALUES ('expense', 5); -- id 4 for mail
INSERT INTO stat (hit_points, defense_points) VALUES (10, 2); -- id 4 for mail
INSERT INTO equipment (name, stat_id, storage_id, unit_id) VALUES ('Mail armor', 4, 4, 2); -- referencing unit id 2 for market

COMMIT;
