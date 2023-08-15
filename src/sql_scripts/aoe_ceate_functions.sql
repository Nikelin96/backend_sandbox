CREATE OR REPLACE FUNCTION get_kingdom_technologies(kingdom_identifier INTEGER) RETURNS TABLE (
    name VARCHAR,
    technology_name VARCHAR,
    technology_description VARCHAR,
    research_start_time TIMESTAMP,
    research_status research_status_type
  ) AS $$ BEGIN RETURN QUERY
SELECT k.name AS name,
  t.name AS technology_name,
  t.description AS technology_description,
  kt.research_start_time AS research_start_time,
  kt.research_status AS research_status
FROM kingdom k
  JOIN kingdom_technology kt ON k.id = kt.kingdom_id
  JOIN technology t ON kt.technology_id = t.id
WHERE k.id = kingdom_id;
END;
$$ LANGUAGE plpgsql;
CREATE OR REPLACE FUNCTION get_technology_dependencies(technology_identifier INTEGER) RETURNS TABLE (
    technology_id INTEGER,
    id INTEGER,
    is_required BOOLEAN,
    unit_id INTEGER,
    skill_id INTEGER,
    equipment_id INTEGER
  ) AS $$ BEGIN RETURN QUERY
SELECT t.id AS technology_id,
  td.id AS id,
  td.is_required AS is_required,
  td.unit_id AS unit_id,
  td.skill_id AS skill_id,
  td.equipment_id AS equipment_id
FROM technology AS t
  LEFT JOIN technology_dependency AS td ON td.technology_id = t.id
WHERE t.id = technology_identifier;
END;
$$ LANGUAGE plpgsql;