# Treasure Sweep Game

#### _C# .NET Team Week for Epicodus_

#### By: **Michelle Morin**, **Jamison Cozart**, **Dusty McCord**, **Patrick Kille** April 6-9, 2020

## Description
_This application allows users to play Treasure Sweep, a battleship-esque game for uncovering hidden treasure on an opponent's beach rather than sinking their ships._

## Specifications:
* When a user registers a new account, the application redirects to a log in view.
* A registered user can log in to their Treasure Sweep account
* When a user logs in to their Treasure Sweep account, the application redirects to a Profile view.
  - If the user does not have a profile, the user is instructed to create a profile. If the user has an existing profile, the profile is displayed.
* A registered, logged in user is able to start a new game
* Once a new game is initalized, both game players' profiles include a link to the game
* The player who started the new game ("player one" hereafter) is initially the current player, and their opponent ("player two" hereafter) is not able to take a turn until player one's turn is complete.
* Player one completes their turn by selecting an available (i.e., not previously selected) tile on the Treasure Sweep board labeled "opponent's board".
* When player one selects an available tile, the tile changes appearance according to whether or not the tile hid a treasure chest, a bomb, or an empty space.
* Player one is able to view their board for reference, to see how their opponent's moves are affecting their beach.
* Once player one completes their turn, player two is able to view the result of player one's turn on their board (labeled "your board" from the perspective of player two). If player one struck a treasure chest, the tile will become an opened treasure chest; a mine becomes an explosion; an empty sandy space becomes a hole in the sand.
* Turns alternate between player one and player two until a game ending condition is met. Game ending conditions include:
  - the first player to uncover all treasure chests on the other player's beach wins
  - a player who digs up a bomb loses
* The Games Turn view automatically refreshes, in case the user does not refresh the page to recognize that it is their turn to play.
* When a game is over, neither player is permitted to take another turn.
* The application includes a leaderboard displaying, for all players who have completed at least one game, the players' win percentage (wins/completed games).
* In a user's profile, all games associated with the user are sorted in descending order by the date/time of the most recent turn.

## Project Concept:
_We defined our minimum viable product (MVP) as an application that allows a user to:_
* Register for a Treasure Sweep account
* Login to their Treasure Sweep account
* Initialize a new game and invite a friend to join the game
* During their turn, select a target on the other user's game board,  
* Display results of each players turn (i.e., open an uncovered treasure chest when they strike treasure, uncover an empty hole when they select an empty space, show an explosion when they select the opponent's burried bomb), and
* Notify each user when the game was over and identify the winning player.

_In addition to the MVP, we defined several stretch goals, including:_
* Integrating a chat feature, so users in a game could send messages back and forth to one another
* Setting up an email mailer, to notify each user when it was their turn to play.
* Randomly generating new game boards each turn
  - We incorporated this stretch goal in our Game model. Each new game object is instantiated with two game boards each having several treasure chests and a mine randomly placed.
* Implementing private and public game options, so that a user could join any public game that does not have a second player
* Hosting the application

## Setup/Installation Requirements

### Install .NET Core

#### on macOS:
* _[Click here](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.106-macos-x64-installer) to download a .NET Core SDK from Microsoft Corp._
* _Open the file (this will launch an installer which will walk you through installation steps. Use the default settings the installer suggests.)_

#### on Windows:
* _[Click here](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.203-windows-x64-installer) to download the 64-bit .NET Core SDK from Microsoft Corp._
* _Open the .exe file and follow the steps provided by the installer for your OS._

#### Install dotnet script
_Enter the command ``dotnet tool install -g dotnet-script`` in Terminal (macOS) or PowerShell (Windows)._

### Install MySQL and MySQL Workbench

#### on macOS:
_Download the MySQL Community Server DMG File [here](https://dev.mysql.com/downloads/file/?id=484914). Follow along with the installer until you reach the configuration page. Once you've reached Configuration, set the following options (or user default if not specified):_
* use legacy password encryption
* set password (and change the password field in appsettings.json file of this repository to match your password)
* click finish
* open Terminal and enter the command ``echo 'export PATH="/usr/local/mysql/bin:$PATH"' >> ~/.bash_profile`` if using Git Bash.
* Verify MySQL installation by opening Terminal and entering the command ``mysql -uroot -p{your password here, omitted brackets}``. If you gain access to the MySQL command line, installation is complete. An error (e.g., -bash: mysql: command not found) indicates something went wrong.

_Download MySQL Workbench DMG file [here](https://dev.mysql.com/downloads/file/?id=484391). Install MySQL Workbench to Applications folder. Open MySQL Workbench and select Local instance 3306 server, then enter the password you set. If it connects, you're all set._

#### on Windows:
_Download the MySQL Web Installer [here](https://dev.mysql.com/downloads/file/?id=484919) and follow along with the installer. Click "Yes" if prompted to update, and accept license terms._
* Choose Custom setup type
* When prompted to Select Products and Features, choose the following: MySQL Server (Will be under MySQL Servers) and MySQL Workbench (Will be under Applications)
* Select Next, then Execute. Wait for download and installation (can take a few minutes)
* Advance through Configuration as follows:
  - High Availability set to Standalone.
  - Defaults are OK under Type and Networking.
  - Authentication Method set to Use Legacy Authentication Method.
  - Set password to epicodus. You can use your own if you want but epicodus will be assumed in the lessons.
  - Unselect Configure MySQL Server as a Windows Service.
* Complete installation process

_Add the MySQL environment variable to the System PATH. Instructions for Windows 10:_
* Open the Control Panel and visit _System > Advanced System Settings > Environment Variables..._
* Select _PATH..._, click _Edit..._, then _Add_.
* Add the exact location of your MySQL installation and click _OK_. (This location is likely C:\Program Files\MySQL\MySQL Server 8.0\bin, but may differ depending on your specific installation.)
* Verify installation by opening Windows PowerShell and entering the command ``mysql -uroot -p{your password here, omitted brackets}``. It's working correctly if you gain access to the MySQL command line. Exit MySQL by entering the command ``exit``.
* Open MySQL Workbench and select Local instance 3306 server (may be named differently). Enter the password you set, and if it connects, you're all set.

### Clone this repository

_Enter the following commands in Terminal (macOS) or PowerShell (Windows):_
* ``cd desktop``
* ``git clone`` followed by the URL to this repository
* ``cd TreasureSweepGame.Solution/TreasureSweepGame``

_Confirm that you have navigated to the TreasureSweepGame directory (e.g., by entering the command_ ``pwd`` _in Terminal)._

_Recreate the ``treasure_sweep`` database using the following commands (in Terminal on macOS or PowerShell on Windows) at the root of the TreasureSweepGame directory:_
* ``dotnet restore``
* ``dotnet build``
* ``dotnet ef database update``

_Run this application by entering the following command in Terminal (macOS) or PowerShell (Windows) at the root of the TreasureSweepGame directory:_
* ``dotnet run`` or ``dotnet watch run``

_To view/edit the source code of this application, open the contents of the TreasureSweepGame.Solution directory in a text editor or IDE of your choice (e.g., to open all contents of the directory in Visual Studio Code on macOS, enter the command_ ``code .`` _in Terminal at the root of the TreasureSweepGame.Solution directory)._

## Technologies Used

* Git
* HTML
* CSS
* C#
* dotnet script
* ASP.NET Core MVC 2.2
* Entity Framework Core 2.2
* Identity
* Razor
* MySQL/SQLite

## License

Licensed under the MIT license.

&copy; 2020 - Michelle Morin, Jamison Cozart, Dusty McCord, Patrick Kille