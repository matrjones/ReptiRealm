class Defecation {
  final String? id;
  final DateTime date;
  final String type;
  final String? comment;

  Defecation({
    this.id,
    required this.date,
    required this.type,
    this.comment,
  });
}
