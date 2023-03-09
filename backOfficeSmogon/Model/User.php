<?php 

class User extends Model 
{
    public function __construct() {}

    public static function checkUser($username)
    {
            $params = array($username);
            $sql = "SELECT * FROM users WHERE username = ?";
            $userChck = self::requestExecute($sql, $params)->fetchAll(PDO::FETCH_ASSOC);
            return $userChck;     
    }

    public static function userConnexion($username)
    {
        $sqlinsert = "SELECT * FROM users WHERE username=:username ";
        $signIn = self::getBdd()->prepare($sqlinsert);
        $signIn->execute(array(
            ':username' => $username,
        ));
        $user = $signIn->fetch(PDO::FETCH_ASSOC);
        return ($user);
    }

    public static function userDisplay()
    {
        $sqlinsert = "SELECT * FROM users
                        INNER JOIN `roles` ON users.role_id = roles.role_id; ";
        $infos = self::requestExecute($sqlinsert);
        $return = $infos->fetchAll(PDO::FETCH_ASSOC);
        return $return;
    }

    public static function getAllUsersPagination($first, $perPage)
    {
        $sqlinsert = "SELECT * FROM users
                        INNER JOIN `roles` ON users.role_id = roles.role_id 
                        ORDER BY users.role_id
                        LIMIT $first, $perPage";
        $infos = self::requestExecute($sqlinsert);
        $return = $infos->fetchAll(PDO::FETCH_ASSOC);
        return $return;
    }

    public static function rightDisplay()
    {
        $sqlinsert = "SELECT role_id, name FROM roles ";
        $infos = self::requestExecute($sqlinsert);
        $return = $infos->fetchAll(PDO::FETCH_ASSOC);
        return $return;
    }

    public static function getAllAdmin()
    {
        $sql = "SELECT users.role_id FROM users";

        return self::requestExecute($sql)->fetchAll(PDO::FETCH_ASSOC);
    }

    public static function updateRight($role,$id)
    {
        $sqlinsert = "UPDATE `users` set role_id=:role_id WHERE user_id=:id ";
        $params=array(
            ':role_id'=>$role,
            ':id'=>$id
        );

        self::requestExecute($sqlinsert,$params);

    }

    public static function getUserRoleById($id)
    {
        $sqlQuery = "SELECT role_id FROM users WHERE user_id=:id";

        $params = array(
            ':id'=>$id
        );

        return self::requestExecute($sqlQuery,$params)->fetch();
    }

}