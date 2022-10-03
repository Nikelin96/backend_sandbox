-- create Spearmen with Spear

INSERT INTO 
    storage(food, gold, wood)
VALUES
    (5, 10, NULL), -- for spearmen
    (NULL, NULL, 5); -- for spear

INSERT INTO 
    stat(hit_points, damage_points)
VALUES
    (100, 1), -- for spearmen
    (0, 10); -- for spear

INSERT INTO 
    unit(name, stat_id, storage_id)
VALUES 
	('Spearmen', 1, 1); -- spearmen

INSERT INTO 
    equipment(name, stat_id, storage_id, unit_id)
VALUES 
	('Spear', 2, 2, 1); -- spear

-- create Market with Mail armor
INSERT INTO 
    storage(wood, food, gold, stone)
VALUES
    (100, 100, 100, 100); -- for market

INSERT INTO 
    stat(hit_points, defense_points)
VALUES
    (100, 1); -- for market

INSERT INTO 
    unit(name, stat_id, storage_id)
VALUES 
	('Market', 3, 3); -- market

INSERT INTO
    market(tax, unit_id)
VALUES
    (30, 2); -- market

INSERT INTO 
    storage(gold)
VALUES
    (5); -- for mail

INSERT INTO 
    stat(hit_points, defense_points)
VALUES
    (10, 2); -- for mail

INSERT INTO
    equipment(name, stat_id, storage_id, unit_id)
VALUES
    ('Mail armor', 4, 4, 2) -- mail