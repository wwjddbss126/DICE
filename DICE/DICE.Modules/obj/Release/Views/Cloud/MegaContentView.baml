   M S B A M L   `   `   `  ���� G  CDICE.Modules, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DICE.Modules.Cloud.BehaviorsDICE.Modules.Cloud.Behaviors  M+clr-namespace:DICE.Modules.ViewModels.CloudDICE.Modules.ViewModels.Cloud  .    (DICE.Modules.Views.Cloud.MegaContentView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  f bDevExpress.Xpf.Grid.v22.1.Core, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aa ]DevExpress.Xpf.Grid.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   e aDevExpress.Xpf.RichEdit.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxre6http://schemas.devexpress.com/winfx/2008/xaml/richedit   j	 fDevExpress.Xpf.LayoutControl.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol 	 ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  ]
 YDevExpress.Mvvm.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aAdxmvvm2http://schemas.devexpress.com/winfx/2008/xaml/mvvm 
  <	behaviors*clr-namespace:DICE.Modules.Cloud.Behaviors 
  =	viewmodel+clr-namespace:DICE.Modules.ViewModels.Cloud 
  $
��1510��$	��915��  � DataContext  1  +DevExpress.Mvvm.UI.ViewModelSourceExtension  *M� * viewmodel:MegaContentViewModel++ � 	Resources 3  -DevExpress.Xpf.RichEdit.MhtToContentConverter   MhtToContentConverter0  *DevExpress.Mvvm.UI.ObjectToObjectConverter   CurrentRowToOpacityConverter   DefaultTarget
   Map   DevExpress.Mvvm.UI.MapItem   Target   Source3  -DevExpress.Xpf.Core.BoolToVisibilityConverter  boolToVisibilityConverter:  4DevExpress.Xpf.Core.BoolToVisibilityInverseConverter $  boolToVisibilityInverseConverter3  -DevExpress.Xpf.Core.EnumToVisibilityConverter  enumToVisibilityConverter&   DevExpress.Xpf.Core.ThemedWindow �� 
TargetType  NewMessageWindowStyle �� Property �� Value  headerImageStyle��%4  &	        &	      &	 +     &	 0     &	 5     &	 :     &	 �         $ 1��.   $ 0�� Z�       �� "  .���  � ��$	 600����  � ��$	 800����  �@�UseLayoutRounding . �� " ��.���  � ��$ 16����  � ��$ 16����  � ��$ Center=�.��� 	 � ColumnDefinitions	 �� $��80 ��� $	��600 ��� $	��300 ��� $	��250 ��� $	��250 �
 � RowDefinitions
 �� $��50 ��� $	��670 ��� $��90 �.+��� $��0��$��0��$��../Assets/evidences.png��$��Uniform=� J� RenderTransformOrigin$ 0.357,0.242&�$��
0 10 0 -10q��� $��0��$��1��$��Collected Evidences��$
��Left=�$��Center=�$��0 25 0 0q�$
��Bold"�$��22(�$��	Noto Sans)��� $��0��$��3��$��../Assets/details.png��$��55��$
��Left=�$��Center=�$��15 10 0 -10q��� $��0��$��3��$��Details��$��Center=�$��Center=�$��
-20 25 0 0q�$
��Bold"�$��22(�$��	Noto Sans)��� $��1��$��2��$��0��$��3��$
��Auto=�$
��Auto=�.��0	 	 *DevExpress.Xpf.LayoutControl.LayoutControl	  $��1��$��2��$��0��$��3�� 	  StretchContentVertically . ! 	  StretchContentHorizontally . 4
 	 .DevExpress.Xpf.LayoutControl.LayoutControlBase 
  Padding$ 0q�$��0 0 0 0q�. 	 (DevExpress.Xpf.LayoutControl.LayoutGroup   Orientation �� *MegaLayoutOrientation+2  ,DevExpress.Mvvm.UI.Interactivity.Interaction   	Behaviors @   :DICE.Modules.Cloud.Behaviors.MegaLayoutControlFlipBehavior     MegaLayoutMode �� *MegaLayoutMode+.+�%  DevExpress.Xpf.Grid.GridControl  -     Name$
 grid��   
Background �#FFFFFF$��13(�   AutoExpandAllGroups . )  #DevExpress.Xpf.Grid.DataControlBase   DesignTimeShowSampleData .     
OrderIndex$ 1��   SelectionMode$ MultipleRow=�$��	Noto Sans)�$��5,20,0,0q�$	��710��$
��2500�� � UseLayoutRounding .    ItemsSource �� *ItemsSource+   SelectedItem ��  �� Path$ CurrentMessage� �� Mode$ TwoWay=� '  !DevExpress.Mvvm.UI.EventToCommand  �  �DevExpress.Mvvm.UI.Interactivity.EventTriggerBase`1[[System.Windows.DependencyObject, WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   	EventName$ CustomColumnDisplayText��   PassEventArgsToCommand$
 TrueX�   Columns $  DevExpress.Xpf.Grid.GridColumn  $  DevExpress.Xpf.Grid.ColumnBase    	FieldName$  Favorite��$  DevExpress.Xpf.Grid.BaseColumn!   Header)  #DevExpress.Xpf.Core.ObjectConverter$! Favorite "   Width2  ,DevExpress.Xpf.Grid.GridColumnWidthConverter$" 20 '#    HorizontalHeaderContentAlignment$# Center=�$   FilterPopupMode$$ CheckedList=�%   EditSettings% 6  0DevExpress.Xpf.Editors.Settings.TextEditSettings  6  0DevExpress.Xpf.Editors.Settings.BaseEditSettings!&   HorizontalContentAlignment$& Center=�  $  Label��$! Label $" 20 $# Center=�$$ CheckedList=�%   $& Center=�  $
  Name��$
! Name $" 40 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  	Extension��$! 	Extension $" 20 $# Center=�$$ CheckedList=�%   $& Center=�  $
  Size��$
! Size $" 20 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  CreationDate��$! Creation Date $" 40 '   	SortOrder] YDevExpress.Data.v22.1, Version=22.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a9  3DevExpress.Data.Design.ColumnSortOrderTypeConverter$' 
Descending (   GroupInterval$( 	DateRange=�$# Center=�$$ DateAlt=�%   $& Center=�  $  ModificationDate��$! Modification Date $" 40 $' 
Descending $( 	DateRange=�$# Center=�$$ DateAlt=�%   $& Center=�  $  SharingType��$! Sharing Type $" 50 $' 
Descending $( 	DateRange=�$# Center=�$$ CheckedList=�%   $& Center=�  $  SharedUserId��$! Shared User (ID) $" 45 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  Owner��$! 
Owner (ID) $" 45 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  Id��$! File ID $" 20 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  ParentId��$! 	Parent ID $" 25 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  Fingerprint��$! Fingerprint $" 35 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $  DownloadURL��$! Download URL $" 35 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $
  Path��$
! Path $" 35 $# Center=�$$ 
ExcelSmart=�%   $& Center=�  $
  Hash��$
! Hash $" 35 $# Center=�$$ 
ExcelSmart=�%   $& Center=�)   View) #  DevExpress.Xpf.Grid.TableView  -   *  Name$
* view��+   HeaderPanelMinHeight$+ 0Q�,   RowMinHeight$, 0Q�-   	AutoWidth- . .   AllowPerPixelScrolling. . &   DevExpress.Xpf.Grid.DataViewBase/   AllowEditing/ .  0   AllowColumnFiltering0 . 1   NavigationStyle$	1 Row=�2   ShowVerticalLines2 . $3   HorizontalScrollbarVisibility$3 Visible=�-4   &AllowHorizontalScrollingVirtualization4 . $	��850��$	��Top=�5   ShowIndicator5 .  6   UseLightweightTemplates$
6 NoneX�$��0,0,0,0q�$ 0.502,1.008&�#7   ShowTargetInfoInDragDropHint7 . 8   SearchPanelAllowFilter8 .  "9   SearchPanelHighlightResults9 . :   ColumnFilterPopupMode$: 
ExcelSmart=�#;   SearchPanelImmediateMRUPopup$
; TrueX�<   ShowSearchPanelMode$< Always=�&   DevExpress.Xpf.Grid.GridViewBase=   ShowGroupedColumns= . >   SearchPanelPosition$> OverGroupPanel=�?   ShowSearchPanelMRUButton? . '@    ShowSearchPanelNavigationButtons@ .  A   ShowSearchPanelResultInfoA . B   SearchPanelNullText$B Search keywords��C   AllowFixedColumnMenuC . !D   ShowCheckBoxSelectorColumnD .    E   Event$"E dxg:TableView.RowDoubleClick��+  %DevExpress.Mvvm.UI.EventToCommandBaseF   CommandF �� *OpenMessageCommand+G   DataRowTemplateG �� .R��� H �� Orientation$H Vertical=�.+�-  'DevExpress.Xpf.Core.MeasurePixelSnapper  ._��� I ��Name$I defaultRowPresenter������ ���� *View.DefaultDataRowTemplate+J   	CellStyleJ �� .  (DevExpress.Xpf.Grid.CellContentPresenter"  K �� TriggersK 	  $��1��$��2��$��3��$��3��$��0,5,0,0q�$	��Top=�$	��740��.+�� $��0,0,0,40q�y��� $ CurrentMessage�L �� 	ConverterL 5   /DevExpress.Xpf.Core.ObjectToVisibilityConverter   M    
HandleListM .  .+�� N 	  AllowHorizontalSizingN . O 	  AllowVerticalSizingO . $ 2��$��0,0,10,0q�.+��� $��0����. $
��Auto=�$
��Auto=�$��0,0,0,0q�.��� ���#FFFFFF$��	5,10,5,10q�	 �� $��* �
 �� $��* ��� $��* �.+�� $��0��
�������.+��� $
��Left=�$
��None=�$	��400��$	��400��$��20,20,20,20q����� *MThumbnailSource+$ OneWay=�� $��1��
�������.+��� $��1��$��20q�$��Center=�$��15(�
���   �P �� 
IsReadOnlyP . $��0q����� *MDataSource+�� $��2��$��3��$	��220��$��
20,40,10,0q�$
��Left=�$��Center=����#0B2161$��	Noto Sans)�$��50��$	��Top=����� *CheckHistoryCommand+���� *SelectedItems+Q �� ElementName$
Q grid��.��� 	 �� $��60 ��� $��* �.+��� $��0��
������ $��Center=�$��50��._��� $��0q����#0B2161$��30��$��30��R �� CornerRadius$R 10��
�������._��� $��25��$��Uniform=�$3��-/DICE.Modules;component/Views/Assets/plus.png��$��8 0 0 0q��� $��1��$��HISTORY��$��12 0 0 0q�S �� TextAlignment$
S Left=�$��Center=�$
��Bold"�
�������$��15(�$��Right=�$��80���� $��2��$��4��$	��220��$��
16,40,14,0q�$
��Left=�$��Center=����#0B2161$��	Noto Sans)�$��50��$	��Top=����� *CheckFileCommand+���� *SelectedItems+$
Q grid��.��� 	 �� $��60 ��� $��* �.+��� $��0��
������ $��Center=�$��50��._��� $��0q����#0B2161$��30��$��30��$R 10��
�������._��� $��25��$��Uniform=�$3��-/DICE.Modules;component/Views/Assets/plus.png��$��8 0 0 0q��� $��1��$��12 0 0 0q�$��EXPORT��$
S Left=�$��Center=�$
��Bold"�
�������$��15(�$��Right=�$��60��