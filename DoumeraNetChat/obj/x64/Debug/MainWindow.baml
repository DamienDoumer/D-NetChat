   M S B A M L   `   `   `  ����I  EDoumeraNetChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DoumeraNetChatDoumeraNetChat  5   	       DoumeraNetChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DoumeraNetChat     5   	   -   5          � Title$  DNetChat��5   	      PaperPlaneBackGround#���   6   $��481.954��6N   $	��712��6_   $	��400��5	   	    � Icon$ Icons\Doumera Icon.png��6   $	��748��6h    � WindowStartupLocation$ CenterScreen=�6w    � 
ResizeMode$ CanResizeWithGrip=�6�    � 	Resources 5
        FriendListBoxContextMenu m� Icon��%t  &	       �� 5   
   .@�5      m� $��EditFriendContextMenuItem��6   -   5        $��Edit this Friend��5   Q    5      �� 5      $��Icons\EditFriend.png��6   5      m� 5      $!��deleteFriendContextMenuItem��6   -   5        $��Delete this Friend��5   T    5      �� 5      $��Icons\DeleteFriend.png��6   5      5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��338* �6   �� 5      $
��225* �6   �� 5      $	��318 �6    � RowDefinitions 5   
   �� 5      $��28 �6   �� 5       $��36 �6   �� 5!      �� 5"      $
��36.3 �6   .+�5,   
   �� -   5         ��Name$ IPLabel��5,      $��Your IP Address is:��6!   $��14(�6?   $��	10,10,0,0q�6M   $��1��5-      $
��Left=�6   $��Center=�68   $	��241��6S   5.      �� 50   
   -   5        	 ��Name$	 FriendListBox��50      $��Stretch=�51      $��10,5,2,-0.5q�60   $��2��6E   
 � ContextMenu#
 �  52      $��Stretch=�53        Sky BackGround#���  6.   55      �� 56   
   -   5        $	 ChatListBox��56      $��Stretch=�6'   $��3,41,10,63.5q�57      $��2��6(   #���  65   $��Stretch=�58      $��1��6.     ListBoxStyle1#���  6?   $��2��59   W   ��56   E   �� *color+5:      �� 5<   
   -   5         ��Name$ ChatTextBox��5<      
�������$
��58.5��5=      $��2.999,0,-73.553,0q�6    $��2��6;    �� TextWrapping$
 Wrap=�6H   $�� ��5>      $��Bottom=�6   $��1��65   �� 5@   
   $��1��6   $��41��6%   $��3,0,-133.183,0q�61   $��2��6I   $	��Top=�6V   .+�5B      �� -   5        $ ChatNameLabel��5B      $��20(�6+   $"��Choose a friend to chat with��69   $��1,1,0,1q�6`   $	��Top=�5C      $��36��6,   $
��Left=�5D      $��290.234��6/   5E      �� 5F   
   -	   5        $��
DragBorder��5F       � ToolTip$ This is Doumera NetChat.��6#   
������ 
������ $��1q�5G      $��-127.052,0,67,0q�6%   $��Stretch=�6>   $��Stretch=�5H      $��2��6-   $��1��6A   5I      �� 5K   
   $��2��6   $��36��6%   $��138.183,0,10,0q�61   $��2��6I   $	��Top=�6V   .+�5L      �� -
   5        $ ChatIPLabel��5L      $�� ��6)   $��20(�64   $	��Top=�6B   $��10,1,1,1q�6Z   5M      �� 5N   
   -   5        $ StatusLabel1��5N      $��No friend chosen��6&   $
��Left=�6A   $��41,5,0,0q�6\   $��3��6n   $	��Top=�6{   $	��250��6�   $��2��6�   �� 5O   
   -   5         ��Name$ 
LoadButton��5O        GlassButtonStyle#���  6%   $- 'load messages you have with this friend��6P   {�.  $��    Load
Messages��6�   $��2��6�   $��38��6�   $��
235,0,0,35q�6�   $��2��6�   $��Bottom=�6�   $
��Left=�6  $��73��6,  $��2��67  6H  �� 5P   
   -   5        $ CreateConnectionButton��5P      #���  61   $��
Create Con��6�   $q kCreate your personal wifi connection to chat with approximate friends if you are not on a network allready.��6�   $��2��5Q      $
��37.5��6   $��156.436,0,88.282,-1q�6-   $��2��6J   $��Bottom=�6W   �� 5R   
   -   5        $ AddFrienButton��5R      #���  6)   $��Add a friend��6q   $' !Add a friend to your friend list.��6�   $��2��6�   $��38��6�   $��
0,0,166,35q�6�   $��2��6�   $��Bottom=�6�   $��Right=�6  $��73��6(  $��2��63  �� 5S   
   
�������$��2��6$   $��10,3.5,10,0q�6/   
�������$	��Top=�6T   $��3��6l   $��1��6�   A� 5T   
   -   5         A�Name$ ConnectionNotifier��5T        RedBall#���  6.   $
��Left=�6O   $��16��6j   $��	10,10,0,0q�6v   $��3��6�   
������ $	��Top=�6�   $��15��6�   o� 5U   
   -   5         o�Name$
 menu��5U      
������ $
��Left=�66   $��28��6Q   $��10,0,0,0q�6]   $	��Top=�6o   $	��113��6�   .@�5V      m� -   5        $��About��5V      $��27��6=   5W      m� 5X      $��Settings��6   .@�5Y      m� -   5         m�Name$ changeNotificationSound��5Y      $��Change notification sound��5Z       5[      �� 5\      $��Icons\Music.png��6    5^      m� 5_      -   5        $ EditUserSettings��5_      $��Edit user settings��6T    5`      �� 5a      $��Icons\UserEdit.png��6    6>   5c      5d      5e      5f      5g      