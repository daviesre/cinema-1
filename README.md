# _Cinema Tracker_

#### By _**Joel Waage, Russ Davies, Shokouh Farvid, and Rick Thornbrugh.**_

## Description

Interactive cinema application that allows theaters to enter movies, locations, and times.  Patrons may search for movies by title, or location.  The patrons can purchase tickets via the website. 

## Setup/Installation Requirements

_File can be cloned from Github @ [https://github.com/Rick1970/cinema].
Created in C# with atom text editor.  Used Nancy framework, and razor view engine.  To run the file after download; first run dnu restore from the command line in order to link to the project.lock.json file. Set up the server by:
In SQLCMD -S "(localdb)\mssqllocaldb"

*CREATE DATABASE cinema;
*GO
*USE cinema;
*GO
*CREATE TABLE movies (id INT IDENTITY(1,1), name VARCHAR(255));
*CREATE TABLE theaters (id INT IDENTITY(1,1), location VARCHAR(255), date_time DATETIME);
*CREATE TABLE movies_theaters (id INT IDENTITY(1,1), movie_id INT, theater_id INT);
*CREATE TABLE users (id INT IDENTITY(1,1), name VARCHAR(255));
*CREATE TABLE tickets (id INT IDENTITY(1,1),  movie_id INT, quantity INT;);
*CREATE TABLE users_tickets (id INT IDENTITY(1,1), user_id INT, ticket_id INT);
*GO
*Open SMSS. Backup the cinema file.  Then restore the cinema file.  Rename as cinema_test during restore.  A mirror test copy will be created. _

## Known Bugs
_None known._

## Support and contact details

_Contact the authors at [rthornbrug@gmail.com] and [russdavies392@gmail.com]._

## Technologies Used

_Atom text editor, in C#, with Nancy framework and razor view engine, xunit testing, Sql server, running on Kestrel server._

### License

*MIT License

*Copyright (c) [2016]
