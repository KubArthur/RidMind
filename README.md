# RidMind
Travail complémentaire - Projet Programmation système

<h1>Présentation RidMind</h1>

RidMind est un mini-jeu de pari entre amis. Le but est de deviner à travers un quiz, les réponses de ces adversaires afin de gagner de point.

RidMind dispose également d’une partie pour mettre en place des quizzes.

Chaque élément sur lequel vous passez la souris est détaillé en bas droite avec plus de détails.

Pour débuter une session, il faut entrer les informations nécessaires afin de devenir soit hôte, soit invité.

Le joueur qui lance la session fait office de serveur pour les autres et peut paramétrer le nombre de joueurs qui peuvent entrer dans la session ainsi que le nombre de tours de jeux, le quiz a lancé, et le temps entre chaque question.

La partie ne peut démarrer que lorsque tous les joueurs nécessaires sont présents. Chaque joueur dispose de sa propre fenêtre.

Bug : veuillez vous assurer que le nombre de tours ne soit pas supérieur au nombre de question !

À chaque tour de jeu, les joueurs reçoivent sur leur fenêtre de jeu la même question à laquelle ils doivent choisir entre oui ou non, puis doivent estimer le nombre de oui total des joueurs avant la fin du chronomètre.

Si personne n’a bien estimé ce nombre, les joueurs les plus proches de ce nombre par score inférieur à ce nombre, uniquement gagnant 1 point.

Lorsque le nombre de tours défini par le joueur qui a lancé la partie est terminé, le gagnant est le joueur (ou les joueurs) ayant le plus de points. 

<h1>Notice technique</h1>
<h2>Diagramme d'activité de RidMind</h2>
<img src="https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F9819b104-c72c-4f33-a8de-f328bb6e5c58%2Fda.RidMind.drawio_(1).png?id=5a8916d7-6c6f-454f-a5a7-b4d45a4013fc&table=block&spaceId=1688fadd-cbb2-4841-8dfd-879c8c4ea36d&width=2000&userId=a01ff20c-cd8d-4655-8041-f08b17481938&cache=v2">



<h2>Diagramme de classe de RidMind</h2>
<img src="https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F67518a2f-62ac-4e09-b500-49ca28c824b0%2Fdc.RidMind.drawio.png?table=block&id=1185abd0-80de-467c-90c4-9de97fbf7d6f&spaceId=1688fadd-cbb2-4841-8dfd-879c8c4ea36d&width=2000&userId=a01ff20c-cd8d-4655-8041-f08b17481938&cache=v2">

L'application a été développée via le Framework .Net WpF et en language de programmation C#. Cette dernière suit une architecture MVVM.

Le modèle MVVM (Model-View-ViewModel) est un modèle de conception architecturale d’interfaces utilisateur permettant de découpler l’interface utilisateur et le code qui ne lui est pas associé. Cela permet de rajouter des fonctionnalités ou de modifier un ModelView sans casser le code.

À noté aussi que le design pattern Singleton a été utilisé afin d'instancié la classe Values une fois. Cela permet de récupérer les données et de les utiliser partout dans l'application.

Les connexions se font lorsqu'un utilisateur crée une session "Run" et qu'un autre utilisateur rejoins "Join".

Des Thread sont lancés afin de rafraîchir les pages dès le moment où il y a une interaction entre le client et le serveur.

Des fonctions async ont été mis en place dans les classes Client et Server afin de s'assurer que l'interface cliente et serveur soient toujours actualisé.

<h2>Diagramme de séquence de l'intéraction client/serveur</h2>
<img src="https://www.notion.so/image/https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fsecure.notion-static.com%2F641c61f8-30e6-4e68-a8b9-9c4bc3cf9648%2Fds.RidMind.drawio.png?id=9bdd77e0-36d3-490a-9567-ab006facd0d6&table=block&spaceId=1688fadd-cbb2-4841-8dfd-879c8c4ea36d&width=2000&userId=a01ff20c-cd8d-4655-8041-f08b17481938&cache=v2">

Lorsque le client souhaite se mettre à jour, il envoie une requête au serveur et celui-ci lui répond via un système de code comme dans une requête http Get. C'est toujours le client qui initie l'action. Le serveur ne fait que de répondre. Les interactions sont stockées dans les classes Client et Server. Ces dernières interviennent jusqu'à la fin de la session.

Update : rajout d'un dossier "Form" dans le dossier RidMind.exe
