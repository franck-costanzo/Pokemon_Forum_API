<?php

abstract class Model {

    protected static $bdd;
    
    // Effectue la connexion à la BDD
    // Instancie et renvoie l'objet PDO associé 
    protected static function getBdd() 
    {
        //version locale
        $servername = '127.0.0.1';
        $username = 'root';
        $password = '';
        $db = 'Smogon_Forum';

        try
        {
            self::$bdd = new PDO( "mysql:host=$servername;dbname=$db", $username, $password,
                            array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION) );
            return self::$bdd;
        }
        catch (PDOException $e)
        {
            echo "Connection failed: " . $e->getMessage();
        }
        
    }

    // Exécute une requête SQL éventuellement paramétrée
    protected static function requestExecute($sql, $params = null) 
    {
        if ($params == null) 
        {
            $result = self::getBdd()->query($sql);    // exécution directe
        }
        else 
        {
            $result = self::getBdd()->prepare($sql);  // requête préparée
            $result->execute($params);
        }
        return $result;
    }

    public static function getLastInsertedId()
    {
        $id = self::$bdd->lastInsertId();
        return $id;
    }

    

}