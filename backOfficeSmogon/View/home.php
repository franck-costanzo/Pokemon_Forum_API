<?php if(isset($_SESSION["users"])) : ?>
    <?php header('Location: admin'); ?>
<?php else : ?>    
<main id="loginPage">
    <form  method="post" id="loginForm">

        <h1>Connexion</h1>

        <label for="username" class="placeholder">Username</label>
        <input id="username" class="input" type="text" name="username" required />


        <label for="password" class="placeholder">Password</label>
        <input id="password" class="input" type="password" name="password" required /> 

        <input name='signIn' type="submit" class='submit' value='connexion'  id="loginSubmit">

    </form>
</main>
<?php endif; ?>