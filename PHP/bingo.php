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
//bij 'bingo = true' is er bingo gevallen en is de game over
$bingo = FALSE;
$kaartGroote = 6;
//een array die alle getrokken nummers gaat bevatten
$getrokkenNummers = array();
//2D array dat de bingokaart is
$bingokaart = drawKaart($kaartGroote);
//een array die alle getallen gaat bevatten die in de bingorij/collom zitten
$getallenDieBingoMaken = [];

//begin van het spel
while(!$bingo){
	
	//er wordt een uniek nummer getrokken
	$huidigNummer = nieuwNummer($bingokaart, $getrokkenNummers);
	array_push($getrokkenNummers, $huidigNummer);
	
	//er wordt bijgehouden hoeveel getallen er al in een rij/collom zijn gevallen
	for($i = 1; $i < 7; $i++){
		for($j = 1; $j < 7; $j++){
			if($bingokaart[$i][$j] == $huidigNummer){
				$bingokaart[$i][7]++;
				$bingokaart[7][$j]++;
			}
		}
	}

	//check of er op een rij/collom bingo is gevallen
	if(in_array(6, array_column($bingokaart, 7))){
		//nummer opvragen op welke rij de bingo ligt
		$rijNummer = array_search(6, array_column($bingokaart, 7));
		$getallenDieBingoMaken = $bingokaart[$rijNummer + 1];
		$bingo = TRUE;
	}	elseif (in_array(6, $bingokaart[7])){
		//nummer opvragen op welke collom de bingo ligt
		$collumnNummer = array_search(6, $bingokaart[7]);
		$getallenDieBingoMaken = array_column($bingokaart, $collumnNummer);
		$bingo = TRUE;
	}
	
}




//printen bingokaart met de bingo groen gekleurt
echo printBingokaartTabel($bingokaart, $getallenDieBingoMaken)."<br>";
echo "Getrokken getallen:<br>";

//alle getrokken nummers printen
foreach($getrokkenNummers as $value){
	echo $value." ";
}

//printen hoeveel getallen er zijn getrokken
echo "<br>Aantal getallen dat er is getrokken: ".count($getrokkenNummers)."<br>";


?>

</body>

<?php
//Het genereren van een bingokaart waar op rij 1 getallen van 10-19 liggen, op rij 2 getallen van 20-29 liggen, enz...
function drawKaart($size){
	$array = array();
	$getrokkenGetallen = array();
	$randomGetalMin = 10;
	$randomGetalMax = 19;
	$indexRow = 0;
	$indexCollum = 0;

	for($indexCollum = 1; $indexCollum <= $size + 1 ; $indexCollum++){
		
			for($indexRow = 1; $indexRow <= $size + 1; $indexRow++){
				if($indexRow == ($size + 1) || $indexCollum == ($size + 1)){
					$randomGetal = 0;
				} else {
				$randomGetal = rand($randomGetalMin, $randomGetalMax);
				
					while(in_array($randomGetal, $getrokkenGetallen)){
						$randomGetal = rand($randomGetalMin, $randomGetalMax);
					}
				}
				$array[$indexCollum][$indexRow] = $randomGetal;
				array_push($getrokkenGetallen, $randomGetal);
			}
		$randomGetalMin += 10;
		$randomGetalMax += 10;
	}
	return $array;
}

//het printen van de bingokaart naar tabelvorm met de bingo groengekleurd
function printBingokaartTabel($bingokaart, $getallenDieBingoMaken){
	$tabelgroote = count($bingokaart);
	$tabelBegin = '<table>';
	$tabelInvul = '';
	$tabelEind = '</table>';
	
	for($indexRow = 1; $indexRow <= $tabelgroote - 1; $indexRow++){
		$tabelInvul .= '<tr>';
		
		for($indexCollum = 1; $indexCollum <= $tabelgroote - 1; $indexCollum++){
			if(in_array($bingokaart[$indexRow][$indexCollum], $getallenDieBingoMaken)){
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

//het trekken van een nieuw nummer dat nog niet is getrokken
function nieuwNummer($bingokaart, $getrokkenNummers){
	$randomNummer = rand(10, 69);
	while(in_array($randomNummer, $getrokkenNummers)){
		$randomNummer = rand(10, 69);
	}
	return $randomNummer;
	
}


?>

</html>
