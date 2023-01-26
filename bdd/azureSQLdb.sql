-- Roles table
CREATE TABLE Roles (
  role_id INT NOT NULL, 
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  PRIMARY KEY (role_id)
);

-- Users table
CREATE TABLE Users (
  user_id INT NOT NULL, 
  username VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  join_date DATE NOT NULL,
  avatar_url VARCHAR(255), 
  isBanned BIT,
  role_id INT,
  PRIMARY KEY (user_id),
  CONSTRAINT FK_Users_role_id_Roles FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
);

-- Teams table
CREATE TABLE Teams (
  team_id INT NOT NULL,
  name VARCHAR(255) NOT NULL,
  link VARCHAR(255) NOT NULL,
  date_created DATE NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (like_id),
  CONSTRAINT FK_Teams_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- BannedUsers table
CREATE TABLE BannedUsers (
    banned_user_id INT NOT NULL,
    user_id INT NOT NULL,
    banned_by_user_id INT NOT NULL,
    ban_start_date DATE NOT NULL,
    ban_end_date DATE NOT NULL,
    reason VARCHAR(255) NOT NULL,
    PRIMARY KEY (banned_user_id),
    CONSTRAINT FK_BannedUsers_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    CONSTRAINT FK_BannedUsers_banned_by_user_id_Users FOREIGN KEY (banned_by_user_id) REFERENCES Users(user_id)
);

-- Topics table
CREATE TABLE Topics (
  topic_id INT NOT NULL,
  name VARCHAR(255) NOT NULL,
  PRIMARY KEY (topic_id)
);

-- Forums table
CREATE TABLE Forums (
  forum_id INT NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  topic_id INT NOT NULL,
  PRIMARY KEY (forum_id),
  CONSTRAINT FK_Forums_topic_id_Topics FOREIGN Key (topic_id) REFERENCES Topics (topic_id) ON DELETE CASCADE
);

-- Sub Forums table
CREATE TABLE SubForums (
  subforum_id INT NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  forum_id INT,
  PRIMARY KEY (subforum_id),
  CONSTRAINT FK_SubForums_forum_id_Forums FOREIGN KEY (forum_id) REFERENCES Forums(forum_id) ON DELETE CASCADE
);

-- User_Moderates_SubForum table
CREATE TABLE User_Moderates_SubForum (
  UMSF_id INT NOT NULL,
  user_id INT NOT NULL,
  subforum_id INT NOT NULL,
  PRIMARY KEY (UMSF_id),
  CONSTRAINT FK_UMSF_user_id_Users FOREIGN KEY (user_id) REFERENCES Users (user_id) ON DELETE CASCADE,
  CONSTRAINT FK_UMSF_subforum_id_SubForums FOREIGN KEY (subforum_id) REFERENCES SubForums (subforum_id) ON DELETE CASCADE
);

-- Threads table
CREATE TABLE Threads (
  thread_id INT NOT NULL,
  title VARCHAR(255) NOT NULL,
  create_date DATE NOT NULL,
  last_post_date DATE,
  user_id INT NOT NULL,
  subforum_id INT NULL,
  PRIMARY KEY (thread_id),
  CONSTRAINT FK_Threads_user_id_Users FOREIGN KEY (user_id) REFERENCES Users (user_id) ON DELETE CASCADE,
  CONSTRAINT FK_Threads_subforum_id_SubForums FOREIGN KEY (subforum_id) REFERENCES SubForums (subforum_id) ON DELETE CASCADE
);

-- Posts table
CREATE TABLE Posts (
  post_id INT NOT NULL,
  content TEXT NOT NULL,
  create_date DATE NOT NULL,
  thread_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (post_id),
  CONSTRAINT FK_Posts_thread_id_Threads FOREIGN KEY (thread_id) REFERENCES Threads(thread_id) ON DELETE CASCADE,
  CONSTRAINT FK_Posts_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION
);

-- Likes table
CREATE TABLE Likes (
  like_id INT NOT NULL,
  post_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (like_id),
  CONSTRAINT FK_Likes_post_id_Posts FOREIGN KEY (post_id) REFERENCES Posts(post_id) ON DELETE CASCADE,
  CONSTRAINT FK_Likes_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE NO ACTION
);

-- Roles fixtures
INSERT INTO Roles (role_id, name, description)
VALUES 
(1, 'admin', 'Site administrator'),
(2, 'moderator', 'Forum moderator'),
(3, 'user', 'Regular user');

-- Users fixtures
INSERT INTO Users (user_id, username, password, email, join_date, avatar_url, isBanned, role_id)
VALUES  
(1, 'admin', 'password', 'admin@example.com', '2022-01-01', 'https://example.com/admin.jpg', 0, 1),
(2, 'moderator', 'password', 'moderator@example.com', '2022-01-02', 'https://example.com/moderator.jpg', 0, 2),
(3, 'user1', 'password', 'user1@example.com', '2022-01-03', 'https://example.com/user1.jpg', 0, 3),
(4, 'user2', 'password', 'user2@example.com', '2022-01-04', 'https://example.com/user2.jpg', 0, 3),
(5, 'user3', 'password', 'user3@example.com', '2022-01-05', 'https://example.com/user3.jpg', 1, 3),
(6, 'user4', 'password', 'user4@example.com', '2022-01-06', 'https://example.com/user4.jpg', 1, 3);

-- Teams
INSERT INTO Teams (team_id, name, link, date_created, user_id) 
VALUES 
(1, 'Admin Team 1', 'https://example.com/admin-team1', '2022-01-02', 1),
(2, 'Admin Team 2', 'https://example.com/admin-team2', '2022-01-03', 1),
(3, 'Moderator Team 1', 'https://example.com/moderator-team1', '2022-01-04', 2),
(4, 'Moderator Team 2', 'https://example.com/moderator-team2', '2022-01-05', 2),
(5, 'Team 1', 'https://example.com/team4', '2022-01-03', 3),
(6, 'Team 2', 'https://example.com/team5', '2022-01-04', 3),
(7, 'Team 3', 'https://example.com/team3', '2022-01-03', 3),
(8, 'Team 1', 'https://example.com/team6', '2022-01-05', 4),
(9, 'Team 2', 'https://example.com/team7', '2022-01-06', 4),
(10, 'Team 4', 'https://example.com/team4', '2022-01-04', 4),
(11, 'Team 5', 'https://example.com/team5', '2022-01-05', 5),
(12, 'Team 6', 'https://example.com/team6', '2022-01-06', 6);

-- BannedUsers fixtures
INSERT INTO BannedUsers (banned_user_id, user_id, banned_by_user_id, ban_start_date, ban_end_date, reason)
VALUES 
(3, 5, 1, '2022-01-05', '2022-03-05', 'Spamming'),
(4, 6, 2, '2022-02-05', '2022-04-05', 'Trolling');

-- Topic fixtures
INSERT INTO Topics (topic_id, name) 
VALUES 
(1, 'Pokemon'),
(2, 'Strategies and Move Sets'),
(3, 'Tournaments and Events'),
(4, 'Clans and Teams');

-- Forums fixtures
INSERT INTO Forums (forum_id, name, description, topic_id) 
VALUES 
(1, 'Pokemon Showcase', 'A place to show off your Pokemon teams and discuss different Pokemon strategies', 1),
(2, 'Pokemon Breeding', 'Discuss Pokemon breeding techniques, share breeding projects and ask for help with breeding', 1),
(3, 'Move Sets and Strategies', 'Discuss and share move sets and strategies for different Pokemon', 2),
(4, 'Tournament Discussion', 'Discuss upcoming tournaments and events and share your experiences from past tournaments', 3),
(5, 'Clan Recruitment', 'Recruit new members for your clan or find a clan to join', 4),
(6, 'Team Building', 'Discuss team building strategies and share your teams with others', 4);

-- SubForums fixtures
INSERT INTO Subforums (subforum_id, name, description, forum_id) VALUES
(1, 'Team Showcase', 'A place to show off your Pokemon teams and get feedback from others', 1),
(2, 'Pokemon Analysis and Discussion', 'A place to discuss and analyze different Pokemon and their roles in battle', 1),
(3, 'Breeding Techniques and Tips', 'A place to share and learn about different breeding techniques and tips', 2),
(4, 'Breeding Projects and Progress', 'A place to share and discuss your breeding projects and progress', 2),
(5, 'Move Set Discussion', 'A place to discuss and share different move sets for Pokemon', 3),
(6, 'Strategies for Specific Pokemon', 'A place to discuss and share strategies for specific Pokemon', 3),
(7, 'Upcoming Tournaments', 'A place to discuss upcoming tournaments and events', 4),
(8, 'Tournament Results and Recap', 'A place to share results and recap past tournaments', 4),
(9, 'Looking for Clan', 'A place for individuals to post and find a clan to join', 5),
(10, 'Clan Recruitment', 'A place for clans to recruit new members', 5),
(11, 'Team Building Strategies', 'A place to discuss and share team building strategies', 6),
(12, 'Sharing Teams', 'A place to share your teams with others for feedback and critique', 6);

-- Threads fixtures
INSERT INTO Threads (thread_id, title, create_date, last_post_date, user_id, subforum_id) 
VALUES
(1, 'My new competitive team', '2022-01-01', '2022-01-01', 1, 1),
(2, 'Showcasing my top performing team', '2022-02-01', '2022-02-01', 2, 1),
(3, 'Analysis of the current metagame', '2022-03-01', '2022-03-01', 3, 2),
(4, 'Advice on breeding a shiny Charmander', '2022-04-01', '2022-04-01', 4, 3),
(5, 'Updates on my breeding project', '2022-05-01', '2022-05-01', 5, 4),
(6, 'Discussion on the best move set for Gyarados', '2022-06-01', '2022-06-01', 6, 5),
(7, 'Strategies for using specific Pokemon', '2022-07-01', '2022-07-01', 7, 6),
(8, 'Upcoming tournaments in my area', '2022-08-01', '2022-08-01', 8, 7),
(9, 'Tournament results and recap', '2022-09-01', '2022-09-01', 9, 8),
(10, 'Looking for a clan to join', '2022-10-01', '2022-10-01', 10, 9),
(11, 'Recruiting new members for our clan', '2022-11-01', '2022-11-01', 11, 10),
(12, 'Team building strategies for beginners', '2022-12-01', '2022-12-01', 12, 11),
(13, 'Sharing my team for feedback and critique', '2023-01-01', '2023-01-01', 13, 12);

-- Posts fixtures
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id)
VALUES
(1, 'Here is my new competitive team. Let me know what you think!', '2022-01-02', 1, 1),
(2, 'Thanks for the feedback everyone, I made some changes to the team based on your suggestions.', '2022-01-03', 1, 1),
(3, 'Here is my top performing team, let me know your thoughts.', '2022-02-02', 2, 2),
(4, 'I've been using this team for a while now and it's been doing great.', '2022-02-03', 2, 2),
(5, 'The current metagame is heavily focused on fast and powerful Pokemon.', '2022-03-02', 3, 3),
(6, 'I think the key to success in this metagame is to have a well-rounded team.', '2022-03-03', 3, 3),
(7, 'I ve had success breeding a shiny Charmander using the Masuda Method.', '2022-04-02', 4, 4),
(8, 'It took me several months but it was worth it in the end.', '2022-04-03', 4, 4),
(9, 'I ve had some successes and failures in my breeding project.', '2022-05-02', 5, 5),
(10, 'I ll be sure to update with any new developments.', '2022-05-03', 5, 5),
(11, 'I ve found that a physical move set with Waterfall and Stone Edge is the most effective for Gyarados.', '2022-06-02', 6, 6),
(12, 'What do you guys think about using a mixed move set instead?', '2022-06-03', 6, 6),
(13, 'In this thread, I ll be sharing strategies for using specific Pokemon.', '2022-07-02', 7, 7),
(14, 'For example, using a Choice Band on a Scizor can be very effective.', '2022-07-03', 7, 7),
(15, 'There are several upcoming tournaments in my area, anyone interested in joining?', '2022-08-02', 8, 8),
(16, 'Here are the results and a recap of the last tournament I participated in.', '2022-09-02', 9, 9),
(17, 'I ve been looking for a clan to join, any recommendations?', '2022-10-02', 10, 10),
(18, 'We are currently recruiting new members for our clan, come join us!', '2022-11-02', 11, 11),
(19, 'In this thread, I ll be sharing team building strategies for beginners.', '2022-12-02', 12, 12),
(20, 'I ll also be sharing my team for feedback and critique.', '2023-01-02', 13, 13);

-- Likes fixtures
INSERT INTO Likes (like_id, post_id, user_id)
VALUES
(1, 1, 2),
(2, 2, 3),
(3, 3, 4),
(4, 4, 5),
(5, 5, 6),
(6, 6, 7),
(7, 7, 8),
(8, 8, 9),
(9, 9, 10),
(10, 10, 11);