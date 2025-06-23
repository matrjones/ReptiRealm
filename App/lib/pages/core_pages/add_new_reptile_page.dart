import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class AddNewReptile extends StatefulWidget {
  const AddNewReptile({super.key});

  @override
  State<AddNewReptile> createState() => _AddNewReptileState();
}


class _AddNewReptileState extends State<AddNewReptile> {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(

      appBar: HeaderBar(),

      body: Align(
        alignment: Alignment.topLeft, // Moves column to the right side
        child: Padding(
          padding: EdgeInsets.all(16.0), // Adds spacing from edges
          child: Column(
            mainAxisSize: MainAxisSize.min,
            // Keeps it compact
            crossAxisAlignment: CrossAxisAlignment.start,
            // Aligns text to the LEFT
            children: [
              Text(
                "Name:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Species:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Morph(s):",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Sex:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "DOB:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "Food type:",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
              Text(
                "CANCEL / SAVE",
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
            ],
          ),
        ),
      ),

      bottomNavigationBar: NavBar(),
    );
  }
}

