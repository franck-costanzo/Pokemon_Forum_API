<?php 

class Like extends Model 
{
    public static function getTop10MostLikesByUser()
    {
        $sql = "SELECT Users.username,
                       COUNT(DISTINCT Likes.like_id) AS total_likes
                FROM Users
                LEFT JOIN Posts ON Users.user_id = Posts.user_id
                LEFT JOIN Likes ON Posts.post_id = Likes.post_id
                WHERE Users.role_id NOT IN (1, 2)
                GROUP BY Users.user_id
                ORDER BY total_likes DESC
                LIMIT 10;";
        $userChck = self::requestExecute($sql)->fetchAll(PDO::FETCH_ASSOC);
        return $userChck;
    }
}