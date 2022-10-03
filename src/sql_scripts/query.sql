SELECT 
	unit.name AS unit_name
	, unit_stat.hit_points AS unit_hp
	, unit_stat.damage_points AS unit_damage
	, unit_stat.defense_points AS unit_defense
	, unit_stat.health_points AS unit_health
	, unit_storage.wood AS unit_wood
	, unit_storage.food AS unit_food
	, unit_storage.gold AS unit_gold
	, unit_storage.stone AS unit_stone  
	, equipment.name AS equipment_name
	, equipment_stat.hit_points AS equipment_hp
	, equipment_stat.damage_points AS equipment_damage
	, equipment_stat.defense_points AS equipment_defense
	, equipment_stat.health_points AS equipment_health
	, equipment_storage.wood AS equipment_wood
	, equipment_storage.food AS equipment_food
	, equipment_storage.gold AS equipment_gold
	, equipment_storage.stone AS equipment_stone
FROM unit AS unit
	JOIN stat AS unit_stat ON unit_stat.id = unit.stat_id
	JOIN storage AS unit_storage ON unit_storage.id = unit.storage_id
	JOIN equipment AS equipment ON equipment.unit_id = unit.id
	JOIN stat AS equipment_stat ON equipment_stat.id = equipment.stat_id
	LEFT JOIN storage AS equipment_storage ON equipment_storage.id = equipment.storage_id;
	

SELECT *
FROM unit AS u
JOIN market AS m
ON m.unit_id = u.id;



-- if spearmen wants to buy mail armor in market
-- the value of storage of mail armor + market tax will be taken from his own storage, and added to market
-- unit_id for mail armor is reassigned to the spearmen


SELECT * FROM
market AS m
LEFT JOIN equipment AS eq ON eq.unit_id = m.unit_id;

SELECT
	unit_storage.wood AS unit_wood
	, unit_storage.food AS unit_food
	, unit_storage.gold AS unit_gold
	, unit_storage.stone AS unit_stone
FROM unit AS unit
JOIN storage AS unit_storage ON unit_storage.id = unit.storage_id
WHERE unit.name LIKE 'Spearmen';


SELECT 
	SUM(s.wood) AS total_wood
	, SUM(s.gold) AS total_gold
	, SUM(s.food) AS total_food
	, SUM(s.stone) AS total_stone
FROM storage AS s
LEFT JOIN unit AS u
ON u.storage_id = s.id
LEFT JOIN equipment AS e
ON e.storage_id = s.id
WHERE u.name LIKE 'Spearmen' OR e.name LIKE 'Mail armor'


SELECT * FROM equipment;
SELECT * FROM storage;