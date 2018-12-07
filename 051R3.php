<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">

<html>

<head>
<title>Inzendopdracht 051R3</title>

<style>
table, th, td {
    border: 1px solid black;
    border-collapse: collapse;
}
th, td {
    padding: 15px;
    text-align: left;
}

</style>

</head>
<body bgcolor="#FFFFFF">

<?php

$bingo = FALSE;
$bingoCheck = "";
$kaartGroote = 6;
$getrokkenNummers = array();
$bingokaart = drawKaart($kaartGroote);
$bingoNummers = array();

while(!$bingo){

	array_push($getrokkenNummers, nieuwNummer($bingokaart, $getrokkenNummers));
	$bingoCheck = checkBingo($bingokaart, $getrokkenNummers, $kaartGroote);
	Switch($bingoCheck){
		case "H1":
			array_push($bingoNummers, $bingokaart[1]);
			$bingo = TRUE;
			break;
		case "H2":
			array_push($bingoNummers, $bingokaart[2]);
			$bingo = TRUE;
			break;
		case "H3":
			array_push($bingoNummers, $bingokaart[3]);
			$bingo = TRUE;
			break;
		case "H4":
			array_push($bingoNummers, $bingokaart[4]);
			$bingo = TRUE;
			break;
		case "H5":
			array_push($bingoNummers, $bingokaart[5]);
			$bingo = TRUE;
			break;
		case "H6":
			array_push($bingoNummers, $bingokaart[6]);
			$bingo = TRUE;
			break;
		case "V1":
			array_push($bingoNummers, array_column($bingokaart, 1));
			$bingo = TRUE;
			break;
		case "V2":
			array_push($bingoNummers, array_column($bingokaart, 2));
			$bingo = TRUE;
			break;
		case "V3":
			array_push($bingoNummers, array_column($bingokaart, 3));
			$bingo = TRUE;
			break;
		case "V4":
			array_push($bingoNummers, array_column($bingokaart, 4));
			$bingo = TRUE;
			break;
		case "V5":
			array_push($bingoNummers, array_column($bingokaart, 5));
			$bingo = TRUE;
			break;
		case "V6":
			array_push($bingoNummers, array_column($bingokaart, 6));
			$bingo = TRUE;
			break;
	}
}
echo printBingokaartTabel($bingokaart, $bingoNummers)."<br>";
echo "Getrokken getallen:<br>";
foreach($getrokkenNummers as $value){
	echo $value." ";
}
echo "<br>Aantal getallen dat er is getrokken: ".count($getrokkenNummers);


?>

</body>

<?php

function drawKaart($size){
	$array = array();
	$getrokkenGetallen = array();
	$randomGetalMin = 10;
	$randomGetalMax = 19;
	$indexRow = 0;
	$indexCollum = 0;

	for($indexCollum = 1; $indexCollum <= $size; $indexCollum++){
		for($indexRow = 1; $indexRow <= $size; $indexRow++){
			$randomGetal = rand($randomGetalMin, $randomGetalMax);
			while(in_array($randomGetal, $getrokkenGetallen)){
				$randomGetal = rand($randomGetalMin, $randomGetalMax);
			}
			$array[$indexCollum][$indexRow] = $randomGetal;
			array_push($getrokkenGetallen, $randomGetal);
		}
		$randomGetalMin += 10;
		$randomGetalMax += 10;
	}
	return $array;
}

function printBingokaartTabel($bingokaart, $bingoNummers){
	$tabelgroote = count($bingokaart);
	$tabelBegin = '<table>';
	$tabelInvul = '';
	$tabelEind = '</table>';
	
	for($indexRow = 1; $indexRow <= $tabelgroote; $indexRow++){
		$tabelInvul .= '<tr>';
		
		for($indexCollum = 1; $indexCollum <= $tabelgroote; $indexCollum++){
			if(in_array($bingokaart[$indexRow][$indexCollum], $bingoNummers[0])){
				$tabelInvul .= "<td  bgcolor=\"LawnGreen\"> ".$bingokaart[$indexRow][$indexCollum].'</td>';
			} else {
				$tabelInvul .= "<td > ".$bingokaart[$indexRow][$indexCollum].'</td>';
			}
		}
		$tabelInvul .= '</tr>';
	}
	
	$tabel = $tabelBegin.$tabelInvul.$tabelEind;
	return $tabel;
}

function nieuwNummer($bingokaart, $getrokkenNummers){
	$randomNummer = rand(10, 69);
	while(in_array($randomNummer, $getrokkenNummers)){
		$randomNummer = rand(10, 69);
	}
	return $randomNummer;
	
}

function checkBingo($bingokaart, $getrokkenNummers, $kaartGroote){
	$geenBingo = "";
	$horizontaleBingo = geheleRij($bingokaart, $getrokkenNummers, $kaartGroote);
	$verticaleBingo = geheleColom($bingokaart, $getrokkenNummers, $kaartGroote);
	if(strcmp($horizontaleBingo, $geenBingo) !== 0){
		return $horizontaleBingo;
	} elseif (strcmp($verticaleBingo, $geenBingo) !== 0){
		return $verticaleBingo;
	} else {
		return $geenBingo;
	}
	
}

function geheleRij($bingokaart, $getrokkenNummers, $kaartGroote){
	if (count(array_intersect($bingokaart[1], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H1";
		return $bingoCheck;
	} elseif(count(array_intersect($bingokaart[2], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H2";
		return $bingoCheck;
	} elseif(count(array_intersect($bingokaart[3], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H3";
		return $bingoCheck;
	} elseif(count(array_intersect($bingokaart[4], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H4";
		return $bingoCheck;
	} elseif(count(array_intersect($bingokaart[5], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H5";
		return $bingoCheck;
	} elseif(count(array_intersect($bingokaart[6], $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "H6";
		return $bingoCheck;
	} else {
		$bingoCheck = "";
		return $bingoCheck;
	}
}

function geheleColom($bingokaart, $getrokkenNummers, $kaartGroote){
	if (count(array_intersect(array_column($bingokaart, 1), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V1";
		return $bingoCheck;
	} elseif(count(array_intersect(array_column($bingokaart, 2), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V2";
		return $bingoCheck;
	} elseif(count(array_intersect(array_column($bingokaart, 3), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V3";
		return $bingoCheck;
	} elseif(count(array_intersect(array_column($bingokaart, 4), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V4";
		return $bingoCheck;
	} elseif(count(array_intersect(array_column($bingokaart, 5), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V5";
		return $bingoCheck;
	} elseif(count(array_intersect(array_column($bingokaart, 6), $getrokkenNummers)) == $kaartGroote){
		$bingoCheck = "V6";
		return $bingoCheck;
	} else {
		$bingoCheck = "";
		return $bingoCheck;
	}
}

?>

</html>