
use PFF


-- game

if exists (select * from sys.tables where name = 'game') drop table game

create table game (
	game_id int identity(1,1) primary key
	,season int not null
	,season_type_code int not null
	,week int not null
	,away_team_id int not null
	,home_team_id int not null
	,away_score int
	,home_score int
)

create unique nonclustered index ix_game_season_type_week_home
on game (
	season
	,season_type_code
	,week
	,home_team_id
)


-- load_game

if exists (select * from sys.tables where name = 'load_game') drop table load_game

create table load_game (
	AwayTeamId int
    ,AwayTeamName varchar(100)
    ,AwayTeamScore int
    ,HomeTeamId int
    ,HomeTeamName varchar(100)
    ,HomeTeamScore int
    ,Week varchar(20)
    ,Season int
)
