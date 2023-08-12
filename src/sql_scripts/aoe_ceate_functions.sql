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