import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/add_new_reptile_page.dart';


class AddNewReptileCard extends StatelessWidget {
  const AddNewReptileCard({super.key});

  @override
  Widget build(BuildContext context) {

    // NAVIGATION TO ADD NEW REPTILE PAGE
    return GestureDetector(
      onTap: () {
        Navigator.push(
          context,
          PageRouteBuilder(
            pageBuilder: (context, animation, secondaryAnimation) => const AddNewReptile(),
            transitionDuration: Duration.zero,
            reverseTransitionDuration: Duration.zero,
          ),
        );
      },


      child: Card(
        margin: const EdgeInsets.symmetric(vertical: 4, horizontal: 8),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
        child: const Center(
          child: Padding(
            padding: EdgeInsets.all(12.0),
            child: Text(
              "+",
              style: TextStyle(
                fontSize: 40, // Adjust this size as needed
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),
          ),
        ),
      ),
    );
  }
}