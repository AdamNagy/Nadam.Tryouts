<?php

$query = json_decode($_POST["query"]);
$controller_propName = "controller";
$action_propName = "action";
$props_propName = "params";

echo "controller \n";
echo $query -> $controller_propName;

echo "\n\naction \n";
echo $query -> $action_propName;

echo "\n\nprops \n";
echo var_dump($query -> $props_propName);

echo "\n\nquery as string\n";
echo $_POST["query"];
?>