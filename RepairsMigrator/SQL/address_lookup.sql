-- PROCEDURE: migration.update_address_lookup()

-- DROP PROCEDURE migration.update_address_lookup();

CREATE OR REPLACE PROCEDURE migration.update_address_lookup(
	)
LANGUAGE 'sql'
AS $BODY$
TRUNCATE migration.property_matching;

INSERT INTO migration.property_matching
SELECT * FROM migration.address_store a
INNER JOIN migration.property_query(a.address) ON 1=1
$BODY$;
