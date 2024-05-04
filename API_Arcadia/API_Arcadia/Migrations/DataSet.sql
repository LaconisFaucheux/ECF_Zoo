
INSERT INTO OpeningHours (DayOfWeek, MorningOpening, MorningClosing, AfternoonOpening, AfternoonClosing)
    VALUES
        ('Lundi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Mardi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Mercredi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Jeudi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Vendredi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Samedi', '09:00:00', '12:00:00', '14:00:00', '19:00:00'),
        ('Dimanche', NULL, NULL, NULL, NULL);

INSERT INTO Healths (State)
    VALUES
        ('En bonne santé'), ('Malade');
        
INSERT INTO ZooServices (Name, Description, FullPrice, ChildPrice)
	VALUES
		('Restauration', 'Le Zoo Arcadia propose un espace de restauration convivial et familial pour vous restaurer au cours de votre visite', NULL, NULL),
        ('Visite guidée', 'Le Zoo Arcadia vous propose une prestation de visite guidée', 0, 0),
        ('Visite en petit train', 'Le Zoo Arcadia vous propose une visite en petit train pour les gros flemmards qui veulent pas marcher', 10, 5);
        
INSERT INTO Reviews (Pseudo, Content, IsValidated, Note)
	VALUES
		('Robert de Boron', 'Le parc est agréable, bien ombragé, idéal pour les promenades avec les enfants. D''autre part les animaux y semblent heureux et en bonne santé!', 1, 5),
		('Geoffroy de Monmouth', 'Super zoo!', 0, 5);

INSERT INTO Diets (Name)
    VALUES
        ('frugivore'), ('carnivore'), ('omnivore'), ('insectivore'), ('herbivore'), ('piscivore');

INSERT INTO Habitats (Name, Description)
    VALUES
      ('savane', 'descriptif de la savane'), ('jungle', 'Descriptif de la jungle'), ('marais', 'Descriptif des marais');

INSERT INTO HabitatImages (Slug, IdHabitat)
    VALUES
        ('savane.png', 1), ('jungle.png', 2), ('marais.png', 3);
      
INSERT INTO SizeUnits (Name, Abbr) 
	VALUES
    ('centimètre', 'cm'), ('mètre', 'm');
    
INSERT INTO WeightUnits (Name, Abbr)
	VALUES
    ('gramme', 'g'), ('kilogramme', 'kg'), ('tonne', 't' );
     
INSERT INTO Speciess (Name, ScientificName, Description, MaleMaxSize, FemaleMaxSize, MaleMaxWeight, FemaleMaxWeight, IdSizeUnit, iDWeightUnit, Lifespan, IdDiet )
	VALUES
		('Lion', 'Panthera Leo', 
        'Le lion est le seul félin véritablement social puisqu''il vit en groupe comprenant généralement une coalition de mâles adultes (souvent apparentés), plusieurs femelles adultes et leurs jeunes. Toutefois l''organisation est de type fission/fusion, c''est à dire que les individus vont et viennent (solitaires ou dispersés en petites unités autonomes) et ne se regroupent qu''occasionnellement. Les femelles font preuve de comportements coopératifs uniques chez les félins : elles mettent bas en synchronie, élèvent les jeunes en communauté en autorisant l''alloparentage et chassent en groupe. Il y a plus de 10 000 ans, les lions étaient nombreux en Europe, en Asie et en Afrique. Aujourd’hui, on ne les rencontre plus qu’en Afrique et au nord-ouest de l’Inde où 200 spécimens subsistent dans la réserve de Gir.',
        2.50, 1.90, 190, 120, 2, 2, 15, 2),        

        ('Orang-Outan de Bornéo', 'Pongo Pygmaeus', 
        'Orang-outan signifie « homme des bois » en malais. L’orang-outan est semi-solitaire et des regroupements peuvent être observés lorsque la nourriture est abondante. Les mâles dominants établissent leur territoire sur ceux, plus petits, des femelles, et tentent d’en interdire l’accès aux autres mâles. Lorsque deux mâles dominants se rencontrent en présence d’une femelle réceptive, la confrontation est agressive. Au contraire des femelles qui établissent leur territoire à proximité de leur lieu de naissance à l’âge adulte, les mâles s’éloignent des individus avec lesquels ils ont des liens de parenté pour  s''implanter sur un territoire. L''orang-outan est le plus grand animal arboricole et son physique est parfaitement adapté à la vie de grimpeur suspendu dans la canopée. Les femelles ne descendent quasi jamais au sol alors que les mâles peuvent y être contraints lorsque les arbres des forêts secondaires ne peuvent supporter leur poids. Son régime alimentaire se compose de plus de 200 fruits différents dont certains à la coque très dure qu''il parvient à briser grâce à ses mâchoires puissantes. Les femelles donnent généralement naissance à leur premier petit vers l''âge de 15 ans et ne seront mères qu''à 4 ou 5 reprises dans leur vie, l''intervalle entre chaque naissance étant particulièrement long chez cette espèce : entre 6 et 7 ans. Chaque soir, l’orang-outan se confectionne un nid de branchages qu’il érige dans une fourche d’arbre à 10 ou 20 mètres de hauteur. L’orang-outan est en danger dans son biotope, principalement à cause de la déforestation pour l''exploitation du bois et la conversion en cultures de palmiers à huile, mais aussi pour le commerce illégal des jeunes en Asie.',
        97, NULL, 85, 60, 1, 2, 50, 1), 

        ('Panthère du Sri Lanka', 'Panthera Pardus Kotiya', 
        'La panthère est réputée pour sa capacité à se fondre dans son environnement. Solitaire, elle marque continuellement son territoire afin de tenir ses congénères à distance. C’est une excellente grimpeuse capable de hisser dans les arbres une proie pesant jusqu''à plus de deux fois son poids. Les premiers jours après la naissance, la femelle reste auprès de ses petits. Mais elle est rapidement contrainte de s''absenter pour chasser, laissant ses jeunes cachés dans la végétation dense, des fissures de rocher ou un arbre creux. Les petits sont extrêmement vulnérables aux prédateurs lorsque leur mère s''absente, c''est pourquoi elle les change de cachette régulièrement. Les jeunes acquièrent leur indépendance vers l''âge d''un an. Tandis que les mâles partent conquérir un nouveau territoire, les femelles demeurent à proximité de leur aire de naissance, empiétant parfois sur le territoire de leur mère. Parmi les 9 sous-espèces de panthères, celles qui peuplent le continent asiatique sont les plus menacées. Elles sont victimes de la déforestation et du braconnage pour leur peau. La pharmacopée traditionnelle asiatique se sert également de leurs os qui remplacent ceux du tigre devenu trop rare.',
        1.90, 1.40, 55, 30, 2, 2, 20, 2),

        ('Loutre d''Europe', 'Lutra Lutra',
        'La loutre d’Europe est une espèce protégée en augmentation qui recolonise progressivement les réseaux hydrographiques désertés depuis un demi-siècle. Présente dans toute l’Europe, la loutre peut vivre jusqu’à 10 ans (longévité maximale observée).',
        85, NULL, 14, NULL, 1, 2, 10, 2),        
        
        ('Rhinocéros Blanc' , 'Ceratotherium Simum',
        'Le rhinocéros blanc est la plus grande des cinq espèces de rhinocéros. Son nom découle d''une mauvaise traduction du terme hollandais "wijd" (ou "wide"/large en anglais), censé décrire la forme de sa lèvre supérieure. C''est la confusion avec le mot "white" qui a donné son nom à l''espèce.
		La forme "carrée" de ses lèvres lui permet de brouter l''herbe et diffère de celle pointue et préhensile des autres espèces de rhinocéros, qui leur sert à arracher les feuilles des arbres et arbustes.',
        1.80, 1.50, 3.50, 1.30, 2, 3, 40, 5),
        
        ('Héron Pourpré', 'Ardea Purpurea',
        'Le corps du Héron pourpré est élancé, ses longues ailes sont étroites et arrondies. Il tient son nom des plumes rousses qui couvrent sa tête. Son cou, ses épaules et son dos sont gris ardoisé. Le dessus de sa tête et les longues plumes de sa crête sont noires. Le Héron pourpré chasse le long des voies d’eau et des terrées. Son activité est essentiellement crépusculaire. Il chasse à l’affût, dissimulé dans la végétation de bord de berge (roseaux, carex…). Il traque ses proies en eau peu profonde.
		Il défend vigoureusement son territoire, gonflant les plumes de son cou et hérissant sa crête.',
        1.50, 1.20, 1400, 600, 1, 1, 25, 6),
        
        ('Gorille des plaines de l''ouest', 'Gorilla Gorilla Gorilla',
        'Le gorille est le plus grand de tous les primates. Il se déplace majoritairement au sol et dort dans un nid qu’il fabrique avec des feuillages en quelques minutes et n’utilise qu’une  seule nuit. Les groupes sont composés d''un mâle dominant, de plusieurs femelles adultes et de leurs jeunes d''âges différents. Chaque animal a une position hiérarchique précise dans son groupe. Malgré son physique impressionnant, le gorille est naturellement farouche et réservé. En cas de dérangement ou d’agression, le mâle cherche à impressionner l’intrus en criant et en frappant sa poitrine avec ses poings.',
        1.80, 1.50, 200, 75, 2, 2, 40, 1);

INSERT INTO SpeciesHabitats (IdSpecies, IdHabitat)
    VALUES
        (1, 1), (2, 2), (3, 2), (4, 3), (5, 1), (6, 3), (7, 2);
        
INSERT INTO Animals (Name, IdSpecies, IdHealth, IsMale)
    VALUES
        ('Mufasa', 1, 1, 1), ('Sarabi', 1, 1, 0),
        ('Bohort', 2, 1, 1), ('Berlewen', 2, 1, 0),
        ('Lancelot', 3, 1, 1), ('Coya', 3, 1, 0),
        ('Arthur', 4, 1, 1), ('Guenièvre', 4, 1, 0),
        ('Anansi', 5, 1, 1), ('Ishtar', 5, 1, 0),
        ('Thot', 6, 1, 1), ('Alex', 6, 1, 0),
        ('Kerchak', 7, 1, 1), ('Coco', 7, 1, 0);

INSERT INTO AnimalImages (Slug, MiniSlug, IdAnimal)
    VALUES
        ('images/animals/638496693869536018_Mufasa.jpg', 'images/animals/638496693869536822_Mufasa_mini.jpg', 1), 
        ('images/animals/638496693873204536_Mufasa.jpg', 'images/animals/638496693873204641_Mufasa_mini.jpg', 2),
        ('images/animals/l_orang_outan1.jpg', 'x', 3), ('images/animals/l_orang_outan2.jpg', 'x', 4),
        ('images/animals/la_pantere1.jpg', 'x', 5), ('images/animals/la_pantere2.jpg', 'x', 6),
        ('images/animals/la_loutre1.jpg', 'x', 7), ('images/animals/la_loutre2.jpg', 'x', 8),
        ('images/animals/le_rhino1.jpeg', 'x', 9), ('images/animals/le_rhino2.jpg', 'x', 10),
        ('images/animals/le_heron1.jpg', 'x', 11), ('images/animals/le_heron2.jpg', 'x', 12),
        ('images/animals/le_gorille1.jpg', 'x', 13), ('images/animals/le_gorille2.jpg', 'x', 14)

INSERT INTO VetVisits (Food, FoodWeight, IdWeightUnit, VisitDate, Observations, IdAnimal, IdVet)
    VALUES
        ('Antilope', 600, 1, '2024-01-21 10:12:28.456', 'RAS Karadoc est en grande forme', 1, 1),
        ('Antilope', 550, 1, '2024-01-21 11:04:28.456', 'RAS Mevanwi est en grande forme', 2, 1),
        ('Coktail de fruits', 700, 1, '2024-02-15 09:15:12.789', 'Bohort est docile et aime la compagnie', 3, 1),
        ('Coktail de fruits', 400, 1, '2024-02-15 09:15:12.789', 'Berlewen est en parfaite santé', 3, 1),
        ('Chevreuil', 550, 1, '2024-03-21 11:04:28.456', 'Ecorchure sur la patte avant droite, à surveiller', 5, 1),
        ('Chevreuil', 450, 1, '2024-03-22 10:04:28.456', 'RAS Coya est en grande forme', 6, 1);
