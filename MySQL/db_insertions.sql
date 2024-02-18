use ZOO;

INSERT INTO opening_hours (day_of_week, morning_opening, morning_closing, afternoon_opening, afternoon_closing)
    VALUES
        ("Lundi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Mardi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Mercredi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Jeudi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Vendredi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Samedi", '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ("Dimanche", NULL, NULL, NULL, NULL);
        
INSERT INTO zoo_service (service_name, service_description, service_full_price, service_child_price, service_special_price)
	VALUES
		("Restauration", "Le Zoo Arcadia propose un espace de restauration convivial et familial pour vous restaurer au cours de votre visite", NULL, NULL, NULL),
        ("Visite guidée", "Le Zoo Arcadia vous propose une prestation de visite guidée", 0, 0, 0),
        ("Visite en petit train", "Le Zoo Arcadia vous propose une visite en petit train pour les gros flemmards qui veulent pas marcher", 10, 5, 8);
        
INSERT INTO review (pseudo, content, is_validated, note)
	VALUES
		("Robert de Boron", "Le parc est agréable, bien ombragé, idéal pour les promenades avec les enfants. D'autre part les animaux y semblent heureux et en bonne santé!", true, 5),
		("Geoffroy de Monmouth", "Super zoo!", false, 5);

INSERT INTO diet (diet_name)
    VALUES
        ("herbivore"), ("carnivore"), ("omnivore"), ("insectivore");

INSERT INTO habitat (habitat_name, habitat_description)
    VALUES
      ("plaines", "descriptif de la plaine"), ("forêts", "Descriptif des forêts"), ("montagne", "Descriptif des Montagnes");
     
INSERT INTO species (species_name, scientific_name, species_description, size_infos, weight_infos, lifespan, species_diet_id )
	VALUES
		("Auroch Reconstitué", "Bos Taurus", 
        "Le recul des zones boisées ajouté à la chasse provoqua la disparition de l’aurochs, l’ancêtre du bœuf domestique. Le dernier individu mourut en Pologne en 1627. L’ensemble des races de bœufs domestiques étant issu de la domestication de l’aurochs, des directeurs de zoo ont tenté de le reconstituer morphologiquement par sélection dite « à rebours » à partir de races primitives : camarguaise, taureau de combat espagnol, highland, gris des steppes de Hongrie, Corse.... La race ainsi obtenue est très rustique et sert entre autres à la gestion de milieux naturels. Elle est reconnue officiellement par le code « race n°30 ». Un syndicat d’éleveurs d’aurochs-reconstitué a été créé en 1995 à la Bergerie Nationale de Rambouillet.",
        "145 cm de hauteur, 250 cm de longueur pour le mâle", "650 kg pour le mâle", 25, 1),

        ("Baudet du Poitou", "Equus Asinus", 
        "Le baudet du Poitou est un âne issu d’une très ancienne sélection. Cette race, qui est la plus ancienne race asine d’Europe, pourrait remonter à l’époque gauloise mais son origine reste un mystère. L’usage du Baudet du Poitou est lié à sa valeur génétique de reproducteur. L’usage du Baudet du Poitou est lié à sa valeur génétique de reproducteur. Il permet de produire, en se croisant avec la jument mulassière poitevine, la mule poitevine d’une qualité exceptionnelle de robustesse et de force. Les mensurations indiquées furent établies en 1923 par le docteur Léon Sausseau. Elles constituent aujourd’hui le standard de la race.",
        "131 à 149 cm au garrot pour les ânesses / 136 à 156 cm au garrot pour les ânes", "320 à 430 kg", 30, 1), 

        ("Bison", "Bison Bonasus", 
        "Le dernier bison d’Europe sauvage fût tué en 1921 dans la forêt de Bialowieza en Pologne. En 1952, des réintroductions ont eu lieu en Pologne à partir d’animaux captifs. Aujourd’hui, le bison d’Europe y vit en semi-liberté ou en liberté totale. Le bison sauvage peut vivre jusqu’à 22 ans (longévité maximale observée). L’accouplement se déroule en août-septembre pour une naissance du veau en mai-juin. La gestation dure entre 260 et 270 jours.",
        "180 à 200 cm de hauteur, 250 à 350 cm de longueur", "800 à 900 kg pour le bison, 500 à 600 kg pour la bisonne", 20, 1),

        ("Ours", "Ursus Arctos Arctos",
        "Ne vous fiez pas à son allure pataude ! Ce roi de la montagne est un excellent marcheur, coureur, grimpeur et nageur. Il peut parcourir jusqu'à 20 km en une nuit, atteindre les 50 km/h en pointe de course. Il hiberne longuement, de mi-novembre à mi-mars, mais ne dort pourtant que d'un oeil...",
        "1m70 à 2m20 debout / 90 à 110 cm au garrot", "jusqu'à 240 kg pour le mâle", 30, 2);

INSERT INTO species_habitat (species_id, habitat_id)
    VALUES
        (1, 1), (2, 1), (3, 1), (4, 2), (4, 3);
        
INSERT INTO animal (animal_name, animal_species, is_male)
    VALUES
        ("Karadoc", 1, true), ("Mevanwi", 1, false),
        ("Bohort", 2, true), ("Berlewen", 2, false),
        ("Lancelot", 3, true), ("Galahad", 3, true),
        ("Arthur", 4, true), ("Guenièvre", 4, false);