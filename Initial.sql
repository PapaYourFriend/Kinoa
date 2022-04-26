use Kinoa

GO
CREATE PROCEDURE Initial AS
BEGIN
	INSERT AgeLimits
	VALUES
	('PEGI-3', '/Views/Images/pegi-3.png'),
	('PEGI-7', '/Views/Images/pegi-7.png'),
	('PEGI-12', '/Views/Images/pegi-12.png'),
	('PEGI-16', '/Views/Images/pegi-16.png'),
	('PEGI-18', '/Views/Images/pegi-18.png')

	INSERT Genres
	VALUES 
	('Экшен'),
	('Романтика'),
	('Ужасы'),
	('Комедия')

	INSERT OrderStatus
	VALUES
	('Active'),
	('Cancelled'),
	('Completed')

	INSERT Movies
           ([Name]
           ,[Title]
           ,[Duration]
           ,[Description]
           ,[Rating]
           ,[StartDate]
           ,[Image]
           ,[Hot]
           ,[AgeLimitId]
           ,[GenreId])
     VALUES
    ('Все везде и сразу', 'Фильм Дениелов', CAST(N'2022-04-18 1:45:00.000' AS datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	7.5,
	CAST(N'2022-04-18 00:00:00.000' AS datetime2),
	'/Resourses/Movies/all.jpg',
	0,
	2,
	2),
	('Бэтмен', 'Правда срывает маски', CAST(N'2022-06-18 2:40:00.000' AS datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	8.1,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/batman.jpg',
	1,
	5,
	1),
	('Главный герой', 'Он был рожден для большего', CAST(N'2022-06-18 2:23:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	9,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/hero.jpg',
	1,
	3,
	4),
	('Кими', 'Она не единственная', CAST(N'2022-06-18 2:10:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	6.9,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/kimi.jpg',
	0,
	3,
	1),
	('Морбиус', 'Начало новой саги про вампиров', CAST(N'2022-06-18 2:20:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	6.2,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/morbius.jpg',
	0,
	4,
	3),
	('Соник', 'Он возвращается', CAST(N'2022-06-18 2:33:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	8.7,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/sonic.jpg',
	0,
	2,
	1),
	('Доктор Стрэндж: Мультивселенная безумия', 'Невозможно постичь все', CAST(N'2022-06-18 2:56:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	8.4,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/strange.jpg',
	1,
	4,
	1),
	('Анчартед', 'На картах не значится', CAST(N'2022-06-18 2:20:00.000' as datetime2), 
	'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.',
	7.5,
	CAST(N'2022-04-18 00:00:00.000' as datetime2),
	'/Resourses/Movies/uncharted.jpg',
	0,
	2,
	1)

	INSERT FilmRooms
	VALUES
	('DOLBY 1', 120, 10, 12, 0, 10),
	('DOLBY 2', 100, 10, 10, 0, 10)

	INSERT MovieSessions 
	VALUES
	(CAST(N'2022-04-26 8:00:00.000' as datetime2),	CAST(N'2022-04-21 10:40:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 1, 2),
	(CAST(N'2022-04-26 11:00:00.000' as datetime2), CAST(N'2022-04-18 13:33:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 1, 6),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2),	CAST(N'2022-04-21 19:10:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 1, 4),
	(CAST(N'2022-04-26 10:00:00.000' as datetime2), CAST(N'2022-04-18 12:10:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 2, 4),
	(CAST(N'2022-04-26 20:00:00.000' as datetime2),	CAST(N'2022-04-21 22:20:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 2, 5),
	(CAST(N'2022-04-26 14:30:00.000' as datetime2), CAST(N'2022-04-18 16:50:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 2, 8),
	(CAST(N'2022-04-26 22:30:00.000' as datetime2), CAST(N'2022-04-18 01:26:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 1, 7),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2), CAST(N'2022-04-18 19:56:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 2, 7),
	(CAST(N'2022-04-26 14:10:00.000' as datetime2), CAST(N'2022-04-18 16:43:00.000' as datetime2),	CAST(N'2022-04-25 00:00:00.000' as datetime2), 1, 3),

	(CAST(N'2022-04-26 8:00:00.000' as datetime2),	CAST(N'2022-04-21 10:40:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 1, 2),
	(CAST(N'2022-04-26 11:00:00.000' as datetime2), CAST(N'2022-04-18 13:33:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 1, 6),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2),	CAST(N'2022-04-21 19:10:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 1, 4),
	(CAST(N'2022-04-26 10:00:00.000' as datetime2), CAST(N'2022-04-18 12:10:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 2, 4),
	(CAST(N'2022-04-26 20:00:00.000' as datetime2),	CAST(N'2022-04-21 22:20:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 2, 5),
	(CAST(N'2022-04-26 14:30:00.000' as datetime2), CAST(N'2022-04-18 16:50:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 2, 8),
	(CAST(N'2022-04-26 22:30:00.000' as datetime2), CAST(N'2022-04-18 01:26:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 1, 7),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2), CAST(N'2022-04-18 19:56:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 2, 7),
	(CAST(N'2022-04-26 14:10:00.000' as datetime2), CAST(N'2022-04-18 16:43:00.000' as datetime2),	CAST(N'2022-04-26 00:00:00.000' as datetime2), 1, 3),

	(CAST(N'2022-04-26 8:00:00.000' as datetime2),	CAST(N'2022-04-21 10:40:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 1, 2),
	(CAST(N'2022-04-26 11:00:00.000' as datetime2), CAST(N'2022-04-18 13:33:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 1, 6),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2),	CAST(N'2022-04-21 19:10:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 1, 4),
	(CAST(N'2022-04-26 10:00:00.000' as datetime2), CAST(N'2022-04-18 12:10:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 2, 4),
	(CAST(N'2022-04-26 20:00:00.000' as datetime2),	CAST(N'2022-04-21 22:20:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 2, 5),
	(CAST(N'2022-04-26 14:30:00.000' as datetime2), CAST(N'2022-04-18 16:50:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 2, 8),
	(CAST(N'2022-04-26 22:30:00.000' as datetime2), CAST(N'2022-04-18 01:26:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 1, 7),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2), CAST(N'2022-04-18 19:56:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 2, 7),
	(CAST(N'2022-04-26 14:10:00.000' as datetime2), CAST(N'2022-04-18 16:43:00.000' as datetime2),	CAST(N'2022-04-27 00:00:00.000' as datetime2), 1, 3),

	(CAST(N'2022-04-26 8:00:00.000' as datetime2),	CAST(N'2022-04-21 10:40:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 1, 2),
	(CAST(N'2022-04-26 11:00:00.000' as datetime2), CAST(N'2022-04-18 13:33:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 1, 6),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2),	CAST(N'2022-04-21 19:10:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 1, 4),
	(CAST(N'2022-04-26 10:00:00.000' as datetime2), CAST(N'2022-04-18 12:10:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 2, 4),
	(CAST(N'2022-04-26 20:00:00.000' as datetime2),	CAST(N'2022-04-21 22:20:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 2, 5),
	(CAST(N'2022-04-26 14:30:00.000' as datetime2), CAST(N'2022-04-18 16:50:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 2, 8),
	(CAST(N'2022-04-26 22:30:00.000' as datetime2), CAST(N'2022-04-18 01:26:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 1, 7),
	(CAST(N'2022-04-26 17:00:00.000' as datetime2), CAST(N'2022-04-18 19:56:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 2, 7),
	(CAST(N'2022-04-26 14:10:00.000' as datetime2), CAST(N'2022-04-18 16:43:00.000' as datetime2),	CAST(N'2022-04-28 00:00:00.000' as datetime2), 1, 3)
END
