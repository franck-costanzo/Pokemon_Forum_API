<?php

    require_once 'View/Renderer.php';

    //autoload controllers
    foreach (glob("Controller/*.php") as $filename)
    {
        include $filename;
    }

    //autoload models
    foreach (glob("Model/*.php") as $classname)
    {
        spl_autoload_register(function ($classname) {
            include 'Model/' .$classname . '.php';
        });
    }
    
    //layout because my main layout view is named layout.php, not to be used as an actual page
    if (isset($_GET['url']) && ( $_GET['url'] == "layout" || $_GET['url'] == "Renderer" 
                                || $_GET['url'] == "footer" || $_GET['url'] == "header")) 
    {
        $view = New Renderer("home");
        $view->display(); 
    }
    else if (isset($_GET['url']) && ($_GET['url'] == "users" ))
    {
        $view = New Renderer("users");
        $view->display();
    }
    else if( isset($_GET['url']) )
    {
        //faire un try catch pour les url qui n'existe pas et faire une page 404
        $view = New Renderer($_GET['url']);
        $view->display();
    }
    else
    {
        $view = New Renderer("home");
        $view->display();
    }