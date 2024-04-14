USE ZOO;

SELECT
    a.animal_name,
    CASE WHEN a.is_male 
		THEN "♂" 
        ELSE "♀" 
        END AS Genre,
    s.species_name,
    CASE WHEN a.is_male 
		THEN CONCAT(s.male_max_weight, ' ', w.unit_abbr) 
        ELSE CONCAT(s.female_max_weight, ' ', w.unit_abbr)
        END AS Poids_Max,
    CASE WHEN a.is_male 
		THEN CONCAT(s.male_max_size, ' ', su.unit_abbr)
        ELSE CONCAT(s.female_max_size, ' ', su.unit_abbr)
        END AS Taille_Max,
    CONCAT(s.lifespan, ' ans') AS Longévité,
    d.diet_name,
    GROUP_CONCAT(h.habitat_name SEPARATOR ', ') AS habitats
FROM animal a
JOIN species s ON a.animal_species = s.id
JOIN diet d ON s.species_diet_id = d.id
JOIN species_habitat sh ON s.id = sh.species_id
JOIN habitat h ON sh.habitat_id = h.id
JOIN size_unit su ON s.size_unit = su.id
JOIN weight_unit w ON s.weight_unit = w.id
GROUP BY a.animal_name, s.species_name, d.diet_name, Genre, Poids_Max, Taille_Max, Longévité
ORDER BY s.species_name;
