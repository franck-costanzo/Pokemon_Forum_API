<div>
    <a href="./"><img src="View/Media/logo.svg" alt="Le sel de la vie logo" id="logo"></a>
</div>
<div>
    <h1>Smogon Forum Back Office</h1>
    <?php if(isset($_SESSION["users"])) : ?>        
        <form method="POST">
            <input type="submit" name="deconnexion" 
                   class="header__nav__menu__link" 
                   id="decoBtn" value="Logout"> 
        </form>
    <?php endif; ?>    
</div>