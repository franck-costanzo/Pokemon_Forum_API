<?php require_once 'Controller/userController.php'?>
<main>
<?php 

    $allUsers = user::userDisplay(); 
    $rights = user::rightDisplay(); 
    $top10Users = Like::getTop10MostLikesByUser();
    
    $nbArticles = count($allUsers);
    
    // On détermine le nombre d'articles par page
    $parPage = 5;

    // On calcule le nombre de pages total
    $pages = ceil($nbArticles / $parPage);

    if(isset($_GET['page']) && !empty($_GET['page']))
    {
        $currentPage = (int) strip_tags($_GET['page']);
    }
    else
    {
        $currentPage = 1;
    }

    // Calcul du 1er article de la page
    $premier = ($currentPage * $parPage) - $parPage;
            
?>
    <section class="adminSection">
        <h2>Gestion des utilisateurs</h2>
        <table>
            <thead>
                <tr>
                    <th>Username</th>
                    <!-- <th>Email</th> -->
                    <th>Role</th>
                    <th>Modify</th>
                </tr>
            </thead>
            <tbody>
            <?php $allUserPagination = User::getAllUsersPagination($premier, $parPage) ?>
            <?php foreach ($allUserPagination as $value) : ?>
                <tr>
                    <td> <?=$value['username']?></td>
                    <td> <?=$value['name']?></td>
                    <td>
                        <form method="POST" id="formRight">
                            <input type="hidden" name="id" value=<?=$value['user_id']?>>
                                <select name="updateRight">
                                <?php foreach ($rights as $value2) : ?>
                                    <?php if ($value2['name'] == $value['name']) : ?>
                                        <option value=<?= $value2['role_id'] ?> selected> <?= $value2['name'] ?></option>
                                    <?php else : ?>
                                        <option value=<?= $value2['role_id'] ?>> <?= $value2['name'] ?></option>
                                    <?php endif; ?>
                                <?php endforeach; ?>
                            <input type=submit name='updateUser' value='modify'>
                        </form>        
                    </td>
                </tr>
            <?php endforeach; ?>
            </tbody>
        </table>

        <?php if ($nbArticles > ($parPage+1)) : ?>
        <nav>
            <ul class="pagination">
                <!-- Lien vers la page précédente (désactivé si on se trouve sur la 1ère page) -->
                <li class="page-item <?= ($currentPage == 1) ? "disabled" : "" ?>">
                <a href="./admin?page=<?php  if (($currentPage - 1) == 0)
                                        { echo 1; } 
                                        else 
                                        { echo ($currentPage - 1); } ?>" class="page-link"><</a>
                </li>
                <?php for($page = 1; $page <= $pages; $page++): ?>
                    <!-- Lien vers chacune des pages (activé si on se trouve sur la page correspondante) -->
                    <li class="page-item-numbers <?= ($currentPage == $page) ? "active" : "" ?>">
                        <a href="./admin?page=<?= $page ?>" class="page-link"><?= $page ?></a>
                    </li>
                <?php endfor ?>
                    <!-- Lien vers la page suivante (désactivé si on se trouve sur la dernière page) -->
                    <li class="page-item <?= ($currentPage == $pages) ? "disabled" : "" ?>">
                    <a href="./admin?page=<?= $currentPage + 1 ?>" class="page-link">></a>
                </li>
            </ul>
        </nav>
        <?php endif; ?>
    </section>

    <section class="adminSection">
        <h2>Top 10 User with most Likes</h2>
        <table>
            <thead>
                <tr>
                    <th>Username</th>
                    <th>Number of Likes</th>
                </tr>
            </thead>
            <tbody>
            <?php foreach ($top10Users as $value) : ?>
                <tr>
                    <td> <?=$value['username']?></td>
                    <td> <?=$value['total_likes']?></td>                    
                </tr>
            <?php endforeach; ?>
            </tbody>
        </table>

</main>