-- FUNCTION: migration.property_query(character varying)

-- DROP FUNCTION migration.property_query(character varying);

CREATE OR REPLACE FUNCTION migration.property_query(
	p_short_address character varying)
    RETURNS TABLE(accuracy real, prop_ref character varying, short_address text, rownumber bigint) 
    LANGUAGE 'plpgsql'
    COST 100
    IMMUTABLE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
	return query
	SELECT word_similarity($1,ph.search_address) as accuracy,
     ph.prop_ref, 
	 ph.search_address as short_address
     , row_number() OVER () AS rank  -- greatest similarity first
	FROM   migration.property_searchable ph
	WHERE     word_similarity($1,ph.search_address) is not null
	and ph.search_address % $1  -- !!
	ORDER  BY ph.search_address <-> $1  -- !!
	LIMIT  1;
end;
$BODY$;

ALTER FUNCTION migration.property_query(character varying)
    OWNER TO repairs_admin;

GRANT EXECUTE ON FUNCTION migration.property_query(character varying) TO PUBLIC;

GRANT EXECUTE ON FUNCTION migration.property_query(character varying) TO repairs_admin;

GRANT EXECUTE ON FUNCTION migration.property_query(character varying) TO repairs_migration_user;

