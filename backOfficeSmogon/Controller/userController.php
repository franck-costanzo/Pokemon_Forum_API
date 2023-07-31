<?php

include 'Model/Model.php';
include 'Model/User.php';

if (isset($_POST['signIn'])) 
{ 
    $errors = [];

    // receive all input values from the form
    $password = htmlentities($_POST['password']);
    $username = htmlentities($_POST['username']);

    // form validation:
    // by adding (array_push()) corresponding error unto $errors array
    if(empty($_POST['username'])){ array_push($errors,'please insert your email'); }
    if(empty($_POST['password'])){ array_push($errors,'please insert your password'); }

    // check the database to make sure 
    // a user does already exist with the same login and password
    $checkExists = User::checkUser($username);

    if ( !$checkExists ) { array_push($errors, "Wrong login/password combination"); }    

    if (count($errors) == 0) 
    {    
        $signIn = User::userConnexion($username);
        if ( password_verify($password, $signIn['password']) && $signIn['role_id'] <= 2) 
        {
            session_start();
            $_SESSION['users'] = $signIn; 
            header('Location:admin');       
        }
        else
        {
            array_push($errors, "Wrong login/password combination");
        }
        
    }

}

if (isset($_POST['updateUser'])) 
{
    $userAdminRoles = User::getAllAdmin();
    $count = 0;        

    foreach( $userAdminRoles as $roles)
    {
        if($roles['role_id'] == 1)
        {
            $count++;
        }
    }

    $user_RoleId = User::getUserRoleById($_POST['id'])["role_id"];

    if(!($count == 1 && $user_RoleId == 1 && $_POST['updateRight'] != 1))
    {
        User::updateRight($_POST['updateRight'], $_POST['id']);
    }      
}



/*-------------------------------
        DISCONNECT CHANGE 
--------------------------------*/
if (isset($_POST['deconnexion'])) 
{
  session_start();

  session_destroy();
  header('Location: home');
  exit;
}