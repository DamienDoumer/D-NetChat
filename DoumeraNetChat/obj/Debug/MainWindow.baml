   M S B A M L   `   `   `  ����I  EDoumeraNetChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DoumeraNetChatDoumeraNetChat  5   	       DoumeraNetChat.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DoumeraNetChat     5   	     � Title$  Doumera Net Chat��5   	   
����·�$��483.954��67   $��750.038��6H   $	��400��6X    � Icon$ Icons\Doumera Icon.png��6h   $	��748��6�    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5   
   $
��338* �6   �� 5   
   $
��225* �6   �� 5   
   $	��318 �6    � RowDefinitions 5      �� 5   
   $��28 �6   �� 5   
   $��36 �6   �� 5   
   �� 5   
   $
��36.3 �6   .+�5      �� -   5        $��
AboutLabel��5      $��15,4,0,-3.5q�6   $��About��63   $��1q�5      $��13(�6$   $
��Left=�62   $��51.176��6M   5      �� 5      -   5        $��SettingsLabel��5      $��66.176,4,0,-5q�6!   $��Settings��68   $��1q�5      $��13(�6$   $
��Left=�62   $��57.824��6M   5      �� 5      -   5         ��Name$ IPLabel��5      $��Your IP Address is:��6   $��14(�6;   $��	10,10,0,0q�6I   $��1��5      $
��Left=�6   $��Center=�68   $	��241��6S   5      �� 5!      -   5         ��Name$ FriendListBox��5!      $��Stretch=�6%   $��10,5,2,-0.5q�6C   $��2��6X   $��Stretch=�5"      5#      �� 5$      -   5        $ ChatListBox��5$      $��Stretch=�6#   $��3,41,10,63.5q�6A   $��2��6W   $��Stretch=�5%      $��1��6.   $��2��5&   W   5'      �� 5)      -   5         ��Name$ ChatTextBox��5)      
�������$
��58.5��5*      $��2.999,0,-73.553,0q�6    $��2��6;    �� TextWrapping$
 Wrap=�6H   $�� ��5+      $��Bottom=�6   $��1��65   �� 5-      $��1��6   $��41��6!   $��3,0,-133.183,0q�6-   $��2��6E   $	��Top=�6R   .+�5/   
   �� -   5        $ ChatNameLabel��5/      $��20(�6'   $��Friend��65   $��1,1,0,1q�6F   $	��Top=�50      $��36��6,   $
��Left=�51      $��290.234��6/   52      �� 53      -   5        $��
DragBorder��53      	 � ToolTip$	 This is Doumera NetChat.��6   
������ 
������ $��1q�54      $��-127.052,0,67,0q�6%   $��Stretch=�6>   $��Stretch=�55      $��2��6-   $��1��6A   56      �� 58      $��2��6   $��36��6!   $��138.183,0,10,0q�6-   $��2��6E   $	��Top=�6R   .+�59   
   �� -	   5        $ ChatIPLabel��59      $��192.168.202.130��6%   $��20(�6?   $	��Top=�6M   $��10,1,1,1q�6e   5:      �� 5;      -
   5        $ StatusLabel1��5;      $��No connection established��6"   $
��Left=�6F   $��41,5,0,0q�6a   $��3��6s   $	��Top=�6�   $	��250��6�   $��2��6�   �� 5<      -   5        
 ��Name$
 EmojiButton��5<      $,	 &Choose an emoji to add in your message��6"   $��Emoji��6S   $��2��6c   $
��37.5��6s   $��78.553,0,0,-1q�6�   $��2��6�   $��Bottom=�6�   $
��Left=�6�   $��72.883��6�   �� 5=      -   5        $
 CreateConnectionButton��5=      $��
Create Con��6.   $q	 kCreate your personal wifi connection to chat with approximate friends if you are not on a network allready.��6C   $��2��5>      $
��37.5��6   $��156.436,0,88.282,-1q�6-   $��2��6J   $��Bottom=�6W   �� 5?      -   5        $
 AddFrienButton��5?      $��Add a friend��6%   $'	 !Add a friend to your friend list.��6<   $��2��6h   $
��37.5��6x   $��0,0,10,-0.5q�6�   $��2��6�   $��Bottom=�6�   $��Right=�6�   $��73.282��6�   �� 5@      
�������$��2��6    $��10,3.5,10,0q�6+   
�������$	��Top=�6P   $��3��6h   $��1��6|   A� 5A      -   5         A�Name$ ConnectionNotifier��5A      $
��Left=�6*   $��25��6E   $��10,6,0,0q�6Q   $��3��6c   
���   �$	��Top=�6   $��26��6�   5B      5C      