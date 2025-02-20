import 'package:flutter/material.dart';


class HeaderBar extends StatefulWidget implements PreferredSizeWidget {
  const HeaderBar({super.key});

  @override
  Size get preferredSize => Size.fromHeight(AppBar().preferredSize.height + 1);

  @override
  State<HeaderBar> createState() => _HeaderBarState();
}


class _HeaderBarState extends State<HeaderBar> {

  @override
  Widget build(BuildContext context) {
    return AppBar(
      title: const Text(
        "ReptiRealm",
        style: TextStyle(
          fontSize: 32,
          fontWeight: FontWeight.bold,
          color: Colors.yellow,
        ),
      ),
      centerTitle: true,
      backgroundColor: Colors.black,
    );
  }
}
