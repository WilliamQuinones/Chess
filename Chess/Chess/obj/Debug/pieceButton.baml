   M S B A M L   `   `   `  ����@  <Chess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ChessChess  5          Chess.pieceButton   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Chess     5      -   5        $��UC��5        � 	Resources  5	       
  P1Rook 
 P1Pawn 
 P2Pawn 
 P2Rook  P2Knight  P2Bishop  P2Queen 
 P2King  P1Knight 	 P1Bishop 

 P1King  P1Queen  BackgroundTexture �� ImageSource  P1RookClassy  P1PawnClassy  P2PawnClassy  P2RookClassy  P2KnightClassy  P2BishopClassy  P2QueenClassy  P2KingClassy  P1KnightClassy  P1BishopClassy  P1KingClassy  P1QueenClassy  BackgroundTexture1  P1RookSpook  P1PawnSpook  P2PawnSpook  P2RookSpook  P2KnightSpook  P2BishopSpook   P2QueenSpook ! P2KingSpook " P1KnightSpook # P1BishopSpook $ P1KingSpook % P1QueenSpook & BackgroundTexture2��%9	  &	        &	 .     &	 \     &	 �     &	 �     &	 �     &	     &	 G    &	 u    &		 �    &	
 �    &	     &	 2    &	 f    &	 �    &	 �    &	     &	 :    &	 q    &	 �    &	 �    &	     &	 J    &	 �    &	 �    &	 �    &	     &	 L    &	     &	 �    &	 �    &	     &	  O    &	! �    &	" �    &	# �    &	$      &	% S    &	& �    �� 5
   
   $��Resources/p1Rook.bmp��6   �� 5   
   $��Resources/p1Pawn.bmp��6   �� 5   
   $��Resources/p2Pawn.bmp��6   �� 5   
   $��Resources/p2Rook.bmp��6   �� 5   
   $��Resources/p2Knight.bmp��6!   �� 5   
   $��Resources/p2Bishop.bmp��6!   �� 5   
   $��Resources/p2Queen.bmp��6    �� 5   
   $��Resources/p2King.bmp��6   �� 5   
   $��Resources/p1Knight.bmp��6!   �� 5   
   $��Resources/p1Bishop.bmp��6!   �� 5   
   $��Resources/p1King.bmp��6   �� 5   
   $��Resources/p1Queen.bmp��6    �� 5   
   $  Resources/stardust_@2X.png��6/   �� 5   
   $!��Resources/p1RookClassic.png��6%   �� 5   
   $!��Resources/p1PawnClassic.png��6%   �� 5   
   $!��Resources/p2PawnClassic.png��6%   �� 5   
   $!��Resources/p2RookClassic.png��6%   �� 5   
   $#��Resources/p2KnightClassic.png��6'   �� 5   
   $#��Resources/p2BishopClassic.png��6'   �� 5   
   $"��Resources/p2QueenClassic.png��6&   �� 5   
   $!��Resources/p2KingClassic.png��6%   �� 5   
   $#��Resources/p1KnightClassic.png��6'   �� 5    
   $#��Resources/p1BishopClassic.png��6'   �� 5!   
   $!��Resources/p1KingClassic.png��6%   �� 5"   
   $"��Resources/p1QueenClassic.png��6&   �� 5#   
   $ Resources/space.jpg��60   �� 5$   
   $��Resources/p1RookSpook.png��6$   �� 5%   
   $��Resources/p1PawnSpook.png��6$   �� 5&   
   $��Resources/p2PawnSpook.png��6$   �� 5'   
   $��Resources/p2RookSpook.png��6$   �� 5(   
   $!��Resources/p2KnightSpook.png��6&   �� 5)   
   $!��Resources/p2BishopSpook.png��6&   �� 5*   
   $ ��Resources/p2QueenSpook.png��6%   �� 5+   
   $��Resources/p2KingSpook.png��6$   �� 5,   
   $!��Resources/p1KnightSpook.png��6&   �� 5-   
   $!��Resources/p1BishopSpook.png��6&   �� 5.   
   $��Resources/p1KingSpook.png��6$   �� 5/   
   $ ��Resources/p1QueenSpook.png��6%   �� 50   
   $ Resources/wood.jpg��60   51      .��53      � .+�55   
   �� -   5         ��Name$ button��55      $��92��6"   $��92��6-   ��69   ��  �� ElementName$ UC�� �� Path$ ButtonBackground�.��56      ��  �� Orientation$ Vertical=�6   $��75��60   $��70��6<   $��Center=�6G   $��Center=�6b   .+�57      �� ��6   �� $ UC��$ Image���58      �� $ UC��$ 
ImageWidth���59      �� $ UC��$ ImageHeight��� 5:      $��Center=�6G   $��Bottom=�6d   
�������$��10(�6�   $
��Bold"�6�   ��6   �� $ UC��$
 Text�6�   5;      5<      5>      5?      