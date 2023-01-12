CREATE SCHEMA IF NOT EXISTS `Smogon_Forum` DEFAULT CHARACTER SET utf8mb4 ;

USE `Smogon_Forum` ;

-- Roles table
CREATE TABLE Roles (
  role_id INT AUTO_INCREMENT NOT NULL, 
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  PRIMARY KEY (role_id)
);

-- Users table
CREATE TABLE Users (
  user_id INT AUTO_INCREMENT NOT NULL, 
  username VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  join_date DATE NOT NULL,
  role_id INT,
  isBanned BOOLEAN,
  PRIMARY KEY (user_id),
  CONSTRAINT FK_Users_role_id_Roles FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
);

-- BannedUsers table
CREATE TABLE BannedUsers (
    banned_user_id INT NOT NULL AUTO_INCREMENT,
    user_id INT NOT NULL,
    banned_by_user_id INT NOT NULL,
    ban_start_date DATE NOT NULL,
    ban_end_date DATE NOT NULL,
    reason VARCHAR(255) NOT NULL,
    PRIMARY KEY (banned_user_id),
    CONSTRAINT FK_BannedUsers_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    CONSTRAINT FK_BannedUsers_banned_by_user_id_Users FOREIGN KEY (banned_by_user_id) REFERENCES Users(user_id)
);


-- Forums table
CREATE TABLE Forums (
  forum_id INT AUTO_INCREMENT NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  PRIMARY KEY (forum_id)
);

-- Sub Forums table
CREATE TABLE SubForums (
  subforum_id INT AUTO_INCREMENT NOT NULL,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  forum_id INT,
  PRIMARY KEY (subforum_id),
  CONSTRAINT FK_SubForums_forum_id_Forums FOREIGN KEY (forum_id) REFERENCES Forums(forum_id) ON DELETE CASCADE
);

-- Threads table
CREATE TABLE Threads (
  thread_id INT AUTO_INCREMENT NOT NULL,
  title VARCHAR(255) NOT NULL,
  create_date DATE NOT NULL,
  last_post_date DATE NOT NULL,
  user_id INT NOT NULL,
  forum_id INT NULL,
  subforum_id INT NULL,
  PRIMARY KEY (thread_id),
  CONSTRAINT FK_Threads_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
  CONSTRAINT FK_Threads_forum_id_Forums FOREIGN KEY (forum_id) REFERENCES Forums(forum_id) ON DELETE CASCADE,
  CONSTRAINT FK_Threads_subforum_id_SubForums FOREIGN KEY (subforum_id) REFERENCES SubForums(subforum_id) ON DELETE CASCADE
);

-- Posts table
CREATE TABLE Posts (
  post_id INT AUTO_INCREMENT NOT NULL,
  content TEXT NOT NULL,
  create_date DATE NOT NULL,
  thread_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (post_id),
  CONSTRAINT FK_Posts_thread_id_Threads FOREIGN KEY (thread_id) REFERENCES Threads(thread_id) ON DELETE CASCADE,
  CONSTRAINT FK_Posts_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- Likes table
CREATE TABLE Likes (
  like_id INT AUTO_INCREMENT NOT NULL,
  post_id INT NOT NULL,
  user_id INT NOT NULL,
  PRIMARY KEY (like_id),
  CONSTRAINT FK_Likes_post_id_Posts FOREIGN KEY (post_id) REFERENCES Posts(post_id) ON DELETE CASCADE,
  CONSTRAINT FK_Likes_user_id_Users FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE
);

-- Roles fixtures
INSERT INTO Roles (role_id, name, description) VALUES (1, 'Admin', 'Can perform all operations');
INSERT INTO Roles (role_id, name, description) VALUES (2, 'Moderator', 'Can perform moderating operations');
INSERT INTO Roles (role_id, name, description) VALUES (3, 'User', 'Can perform basic operations');

-- Users fixtures
INSERT INTO Users (user_id, username, password, email, join_date, role_id, isBanned) 
VALUES (1, 'admin', 'password', 'admin@example.com', '2022-01-01', 1, FALSE);
INSERT INTO Users (user_id, username, password, email, join_date, role_id, isBanned) 
VALUES (2, 'moderator', 'password', 'moderator@example.com', '2022-01-01', 2, FALSE);
INSERT INTO Users (user_id, username, password, email, join_date, role_id, isBanned) 
VALUES (3, 'user', 'password', 'user@example.com', '2022-01-01', 3, FALSE);
INSERT INTO Users (user_id, username, password, email, join_date, role_id, isBanned) 
VALUES (4, 'user2', 'password', 'user2@example.com', '2022-01-01', 3, TRUE);

-- Forums fixtures
INSERT INTO Forums (forum_id, name, description) VALUES (1, 'General Discussion', 'Discuss anything you like here');
INSERT INTO Forums (forum_id, name, description) VALUES (2, 'News and Announcements', 'Official announcements and news');

-- SubForums fixtures
INSERT INTO SubForums (subforum_id, name, description, forum_id) VALUES (1, 'Sub Forum 1', 'First Sub Forum', 1);
INSERT INTO SubForums (subforum_id, name, description, forum_id) VALUES (2, 'Sub Forum 2', 'Second Sub Forum', 1);

-- Threads fixtures
INSERT INTO Threads (thread_id, title, create_date, last_post_date, user_id, forum_id, subforum_id) 
VALUES (1, 'Thread 1', '2022-01-01', '2022-01-02', 3, 1, 1);
INSERT INTO Threads (thread_id, title, create_date, last_post_date, user_id, forum_id, subforum_id) 
VALUES (2, 'Thread 2', '2022-01-03', '2022-01-04', 2, 2, NULL);
INSERT INTO Threads (thread_id, title, create_date, last_post_date, user_id, forum_id, subforum_id) 
VALUES (3, 'Thread 3', '2022-01-05', '2022-01-06', 1, 1, 2);

-- Posts fixtures
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id) VALUES (1, 'Post 1', '2022-01-01', 1, 3);
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id) VALUES (2, 'Post 2', '2022-01-02', 1, 2);
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id) VALUES (3, 'Post 3', '2022-01-03', 2, 1);
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id) VALUES (4, 'Post 4', '2022-01-04', 3, 2);
INSERT INTO Posts (post_id, content, create_date, thread_id, user_id) VALUES (5, 'Post 5', '2022-01-05', 3, 1);

-- Likes fixtures
INSERT INTO Likes (like_id, post_id, user_id) VALUES (1, 1, 2);
INSERT INTO Likes (like_id, post_id, user_id) VALUES (2, 1, 1);
INSERT INTO Likes (like_id, post_id, user_id) VALUES (3, 2, 3);
INSERT INTO Likes (like_id, post_id, user_id) VALUES (4, 5, 1);

-- BannedUsers fixtures
INSERT INTO BannedUsers (banned_user_id, user_id, banned_by_user_id, ban_start_date, ban_end_date, reason) 
VALUES (1, 4, 2, '2022-01-01', '2022-01-31', 'User was mean');