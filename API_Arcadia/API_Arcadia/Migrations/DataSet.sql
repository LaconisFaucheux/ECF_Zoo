
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
        ('En bonne sant�'), ('Malade');
        
INSERT INTO ZooServices (Name, Description, FullPrice, ChildPrice)
	VALUES
		('Restauration', 'Le Zoo Arcadia propose un espace de restauration convivial et familial pour vous restaurer au cours de votre visite', NULL, NULL),
        ('Visite guid�e', 'Le Zoo Arcadia vous propose une prestation de visite guid�e', 0, 0),
        ('Visite en petit train', 'Le Zoo Arcadia vous propose une visite en petit train pour les gros flemmards qui veulent pas marcher', 10, 5);
        
INSERT INTO Reviews (Pseudo, Content, IsValidated, Note)
	VALUES
		('Robert de Boron', 'Le parc est agr�able, bien ombrag�, id�al pour les promenades avec les enfants. D''autre part les animaux y semblent heureux et en bonne sant�!', 1, 5),
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
    ('centim�tre', 'cm'), ('m�tre', 'm');
    
INSERT INTO WeightUnits (Name, Abbr)
	VALUES
    ('gramme', 'g'), ('kilogramme', 'kg'), ('tonne', 't' );
     
INSERT INTO Speciess (Name, ScientificName, Description, MaleMaxSize, FemaleMaxSize, MaleMaxWeight, FemaleMaxWeight, IdSizeUnit, iDWeightUnit, Lifespan, IdDiet )
	VALUES
		('Lion', 'Panthera Leo', 
        'Le lion est le seul f�lin v�ritablement social puisqu''il vit en groupe comprenant g�n�ralement une coalition de m�les adultes (souvent apparent�s), plusieurs femelles adultes et leurs jeunes. Toutefois l''organisation est de type fission/fusion, c''est � dire que les individus vont et viennent (solitaires ou dispers�s en petites unit�s autonomes) et ne se regroupent qu''occasionnellement. Les femelles font preuve de comportements coop�ratifs uniques chez les f�lins : elles mettent bas en synchronie, �l�vent les jeunes en communaut� en autorisant l''alloparentage et chassent en groupe. Il y a plus de 10 000 ans, les lions �taient nombreux en Europe, en Asie et en Afrique. Aujourd�hui, on ne les rencontre plus qu�en Afrique et au nord-ouest de l�Inde o� 200 sp�cimens subsistent dans la r�serve de Gir.',
        2.50, 1.90, 190, 120, 2, 2, 15, 2),        

        ('Orang-Outan de Born�o', 'Pongo Pygmaeus', 
        'Orang-outan signifie � homme des bois � en malais. L�orang-outan est semi-solitaire et des regroupements peuvent �tre observ�s lorsque la nourriture est abondante. Les m�les dominants �tablissent leur territoire sur ceux, plus petits, des femelles, et tentent d�en interdire l�acc�s aux autres m�les. Lorsque deux m�les dominants se rencontrent en pr�sence d�une femelle r�ceptive, la confrontation est agressive. Au contraire des femelles qui �tablissent leur territoire � proximit� de leur lieu de naissance � l��ge adulte, les m�les s��loignent des individus avec lesquels ils ont des liens de parent� pour  s''implanter sur un territoire. L''orang-outan est le plus grand animal arboricole et son physique est parfaitement adapt� � la vie de grimpeur suspendu dans la canop�e. Les femelles ne descendent quasi jamais au sol alors que les m�les peuvent y �tre contraints lorsque les arbres des for�ts secondaires ne peuvent supporter leur poids. Son r�gime alimentaire se compose de plus de 200 fruits diff�rents dont certains � la coque tr�s dure qu''il parvient � briser gr�ce � ses m�choires puissantes. Les femelles donnent g�n�ralement naissance � leur premier petit vers l''�ge de 15 ans et ne seront m�res qu''� 4 ou 5 reprises dans leur vie, l''intervalle entre chaque naissance �tant particuli�rement long chez cette esp�ce : entre 6 et 7 ans. Chaque soir, l�orang-outan se confectionne un nid de branchages qu�il �rige dans une fourche d�arbre � 10 ou 20 m�tres de hauteur. L�orang-outan est en danger dans son biotope, principalement � cause de la d�forestation pour l''exploitation du bois et la conversion en cultures de palmiers � huile, mais aussi pour le commerce ill�gal des jeunes en Asie.',
        97, NULL, 85, 60, 1, 2, 50, 1), 

        ('Panth�re du Sri Lanka', 'Panthera Pardus Kotiya', 
        'La panth�re est r�put�e pour sa capacit� � se fondre dans son environnement. Solitaire, elle marque continuellement son territoire afin de tenir ses cong�n�res � distance. C�est une excellente grimpeuse capable de hisser dans les arbres une proie pesant jusqu''� plus de deux fois son poids. Les premiers jours apr�s la naissance, la femelle reste aupr�s de ses petits. Mais elle est rapidement contrainte de s''absenter pour chasser, laissant ses jeunes cach�s dans la v�g�tation dense, des fissures de rocher ou un arbre creux. Les petits sont extr�mement vuln�rables aux pr�dateurs lorsque leur m�re s''absente, c''est pourquoi elle les change de cachette r�guli�rement. Les jeunes acqui�rent leur ind�pendance vers l''�ge d''un an. Tandis que les m�les partent conqu�rir un nouveau territoire, les femelles demeurent � proximit� de leur aire de naissance, empi�tant parfois sur le territoire de leur m�re. Parmi les 9 sous-esp�ces de panth�res, celles qui peuplent le continent asiatique sont les plus menac�es. Elles sont victimes de la d�forestation et du braconnage pour leur peau. La pharmacop�e traditionnelle asiatique se sert �galement de leurs os qui remplacent ceux du tigre devenu trop rare.',
        1.90, 1.40, 55, 30, 2, 2, 20, 2),

        ('Loutre d''Europe', 'Lutra Lutra',
        'La loutre d�Europe est une esp�ce prot�g�e en augmentation qui recolonise progressivement les r�seaux hydrographiques d�sert�s depuis un demi-si�cle. Pr�sente dans toute l�Europe, la loutre peut vivre jusqu�� 10 ans (long�vit� maximale observ�e).',
        85, NULL, 14, NULL, 1, 2, 10, 2),        
        
        ('Rhinoc�ros Blanc' , 'Ceratotherium Simum',
        'Le rhinoc�ros blanc est la plus grande des cinq esp�ces de rhinoc�ros. Son nom d�coule d''une mauvaise traduction du terme hollandais "wijd" (ou "wide"/large en anglais), cens� d�crire la forme de sa l�vre sup�rieure. C''est la confusion avec le mot "white" qui a donn� son nom � l''esp�ce.
		La forme "carr�e" de ses l�vres lui permet de brouter l''herbe et diff�re de celle pointue et pr�hensile des autres esp�ces de rhinoc�ros, qui leur sert � arracher les feuilles des arbres et arbustes.',
        1.80, 1.50, 3.50, 1.30, 2, 3, 40, 5),
        
        ('H�ron Pourpr�', 'Ardea Purpurea',
        'Le corps du H�ron pourpr� est �lanc�, ses longues ailes sont �troites et arrondies. Il tient son nom des plumes rousses qui couvrent sa t�te. Son cou, ses �paules et son dos sont gris ardois�. Le dessus de sa t�te et les longues plumes de sa cr�te sont noires. Le H�ron pourpr� chasse le long des voies d�eau et des terr�es. Son activit� est essentiellement cr�pusculaire. Il chasse � l�aff�t, dissimul� dans la v�g�tation de bord de berge (roseaux, carex�). Il traque ses proies en eau peu profonde.
		Il d�fend vigoureusement son territoire, gonflant les plumes de son cou et h�rissant sa cr�te.',
        1.50, 1.20, 1400, 600, 1, 1, 25, 6),
        
        ('Gorille des plaines de l''ouest', 'Gorilla Gorilla Gorilla',
        'Le gorille est le plus grand de tous les primates. Il se d�place majoritairement au sol et dort dans un nid qu�il fabrique avec des feuillages en quelques minutes et n�utilise qu�une  seule nuit. Les groupes sont compos�s d''un m�le dominant, de plusieurs femelles adultes et de leurs jeunes d''�ges diff�rents. Chaque animal a une position hi�rarchique pr�cise dans son groupe. Malgr� son physique impressionnant, le gorille est naturellement farouche et r�serv�. En cas de d�rangement ou d�agression, le m�le cherche � impressionner l�intrus en criant et en frappant sa poitrine avec ses poings.',
        1.80, 1.50, 200, 75, 2, 2, 40, 1);

INSERT INTO SpeciesHabitats (IdSpecies, IdHabitat)
    VALUES
        (1, 1), (2, 2), (3, 2), (4, 3), (5, 1), (6, 3), (7, 2);
        
INSERT INTO Animals (Name, IdSpecies, IdHealth, IsMale)
    VALUES
        ('Mufasa', 1, 1, 1), ('Sarabi', 1, 1, 0),
        ('Bohort', 2, 1, 1), ('Berlewen', 2, 1, 0),
        ('Lancelot', 3, 1, 1), ('Coya', 3, 1, 0),
        ('Arthur', 4, 1, 1), ('Gueni�vre', 4, 1, 0),
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
        ('Coktail de fruits', 400, 1, '2024-02-15 09:15:12.789', 'Berlewen est en parfaite sant�', 3, 1),
        ('Chevreuil', 550, 1, '2024-03-21 11:04:28.456', 'Ecorchure sur la patte avant droite, � surveiller', 5, 1),
        ('Chevreuil', 450, 1, '2024-03-22 10:04:28.456', 'RAS Coya est en grande forme', 6, 1);
