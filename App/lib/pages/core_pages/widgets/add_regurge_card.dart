import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:reptirealm/pages/modify_data_pages/add_regurge_page.dart';


class AddRegurgeCard extends StatelessWidget {
  const AddRegurgeCard({super.key});

  @override
  Widget build(BuildContext context) {

    // NAVIGATION TO ADD NEW REPTILE PAGE
    return GestureDetector(
        onTap: () {
          Navigator.push(
            context,
            PageRouteBuilder(
              pageBuilder: (context, animation, secondaryAnimation) => const AddRegurge(),
              transitionDuration: Duration.zero,
              reverseTransitionDuration: Duration.zero,
            ),
          );
        },

      child: Card(
        margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
        child: const Padding(
          padding: EdgeInsets.all(16.0),
          child: Column(
            mainAxisSize: MainAxisSize.min, // Keeps the column size to its children
            mainAxisAlignment: MainAxisAlignment.center, // Centers vertically
            crossAxisAlignment: CrossAxisAlignment.center, // Centers horizontally
            children: [
              Text(
                'Add\nRegurge',
                textAlign: TextAlign.center, // Ensures text is centered
                style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
