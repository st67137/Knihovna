   M S B A M L   `   `   `  ����  KnihovnaZoldak'    !KnihovnaZoldak.PujcovnaKnihWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  PujcovnaKnih��5   	   $	��520��6   $	��520��6.   $
��1150��6;   $
��1150��6K   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $
��0.6* �6   �� 5	      $
��0.2* �6   .+�5   
   �� $��0��6   $��
20,20,20,0q�6%   .+�5      �� $��KNIHOVNY��6   $��14(�6(   $
��Bold"�66   $��Stretch=�6H   $	��Top=�6f   $
��auto��6~   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonKnihovnyCommand+�� 5      $��KNIHY��6   $��14(�6%   $
��Bold"�63   $��Stretch=�6E   $	��Top=�6c   $
��auto��6{   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonKnihyCommand+�� 5      $��ZÁKAZNÍCI��6   $��14(�6)   $
��Bold"�67   $��Stretch=�6I   $	��Top=�6g   $
��auto��6   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonZakazniciCommand+�� 5      $��VÝPŮJČKY��6   $��14(�6(   $
��Bold"�66   $��Stretch=�6H   $	��Top=�6f   $
��auto��6~   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonVypujckyCommand+�� 5      $��KONEC��6   $��14(�6%   $
��Bold"�63   $��Stretch=�6E   $	��Top=�6c   $
��auto��6{   $��40��6�   $��0,60,0,0q�6�   ��6�   �� *ButtonKonecCommand+5      �� 5   
   $��1��6   .+�5      �� $��24(�64   $
��Bold"�6B   $
��Left=�6T   $	��Top=�6o   ��6   �� *LabelNadpis+6o   5      �� 5   
   $��2��6   $��
20,20,20,0q�6%   .+�5      �� $��NOVÝ��6   $��14(�6$   $
��Bold"�62   $��Stretch=�6D   $	��Top=�6b   $
��auto��6z   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonPridejCommand+�� 5      -   5         ��Name$ buttonUprav��5      $��UPRAV��6*   $��14(�6:   $
��Bold"�6H   $��Stretch=�6Z   $	��Top=�6x   $
��auto��6�   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonUpravCommand+{�6�   �� *ButtonUpravIsEnabled+�� 5      $��SMAZAT��6   $��14(�6&   $
��Bold"�64   $��Stretch=�6F   $	��Top=�6d   $
��auto��6|   $��40��6�   $��0,20,0,0q�6�   ��6�   �� *ButtonOdeberCommand+�� 5      -   5         ��Name$ labelKnihovna��5      $��Knihovna��6W   $��16(�6j   $
��Bold"�6x   $��Stretch=�6�   $	��Top=�6�   $��0,0,0,0q�6�   y�6+   �� *FiltrovaniVisibility+6�   �� 5      -   5         ��Name$ comboBoxKnihovny��5      $��Stretch=�6�   $	��Top=�6�   $
��auto��6�   $��28��6�   y�61   �� *FiltrovaniVisibility+ �� SelectedItem 6]   �� *VybranaKnihovna+��6�   �� *ComboBoxKnihovnyItems+6�   �� 5      -   5        $ 
labelKniha��5      $��Kniha��6T   $��16(�6d   $
��Bold"�6r   $��Stretch=�6�   $	��Top=�6�   $��0,0,0,0q�6�   y�6(   �� *FiltrovaniVisibility+6�   �� 5      -   5        $ comboBoxKnihy��5      $��Stretch=�6�   $	��Top=�6�   $
��auto��6�   $��28��6�   y�6.   �� *FiltrovaniVisibility+ 6Z   �� *VybranaKniha+��6�   �� *ComboBoxKnihyItems+6�   �� 5       -   5        $ labelZakaznik��5       $��Zakaznik��6W   $��16(�6j   $
��Bold"�6x   $��Stretch=�6�   $	��Top=�6�   $��0,0,0,0q�6�   y�6+   �� *FiltrovaniVisibility+6�   �� 5!      -   5        $ comboBoxZakaznici��5!      $��Stretch=�6�   $	��Top=�6�   $
��auto��6�   $��28��6�   y�62   �� *FiltrovaniVisibility+ 6^   �� *VybranyZakaznik+��6�   �� *ComboBoxZakazniciItems+6�   �� 5"      -   5        $ buttonFiltr��5"      $��FILTRUJ��6V   $��16(�6h   $
��Bold"�6v   $��Stretch=�6�   $	��Top=�6�   $
��auto��6�   $��36��6�   $��0,20,0,0q�6�   y�6*   �� *FiltrovaniVisibility+��6�   �� *ButtonFiltrujCommand+5#      �� 5%   
   -	   5         ��Name$ listBoxInformace��5%   �   $��1��6z   $
��Auto=�6�   $
��Auto=�6�   $��Consolas)�6  $��14(�6  $��Stretch=�6*  $��Stretch=�6H  $��	0,50,0,30q�6d  ��6   �� *ListBoxInformaceItems+ 6@   �� *SelectedListBoxItem+ �� Mode$ TwoWay=�5'      5(      5)      