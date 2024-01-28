# GooglePhotosTakeoutOrganize 2024
Utilitary to remove short videos of live photos and organize pictures by month

I used google photos for years, but now I want to keep a copy of all my photos in my computer. As google doesn't allow this, i moved to onedrive.

I created this program to solve two problems:<br>
1 - Remove short videos of live photos (keep the image part)<br>
2 - Organize photos by month instead of year like google does

You need to use google takeout before use this program.<br>
After dowload data, you have to uncompress all files and remove all .json files that come together.<br>
Those files contains metadata, but I realize that pictures already have their metadata.<br>
To remove them I search for ".json" in windows explorer search field and then seletecd all and delete.<br>
My case was 80GB of photos and this program worked well.<br>
After process all files, I move the folders to my onedrive Pictures folder.

For developers:
Visual studio 2022
.net8
MetadataExtractor library (to find out taken/creation date from files)
https://github.com/drewnoakes/metadata-extractor

If you have a file outside folders structure, look in the properties of the file for 'Data taken' or 'Media creation' property and put it in the right folder before run to get all things done :)

If you have different needs like bellow, please, send me a message.

1 - Move pictures wihtout date to a different folder<br>
2 - Use year of picture date instead of folder's name<br>
3 - Organize in other way<br>
4 - Custom name of 'Photos of YEAR'<br>
5 - Compile for another plataform (linux)<br>
6 - Anything else
