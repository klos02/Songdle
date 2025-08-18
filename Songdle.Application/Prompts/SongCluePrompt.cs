using System;

namespace Songdle.Application.Prompts;

public static class SongCluePrompt
{
    public static string GetPrompt() => """
    You are a hint generator for a music guessing game. 
The player only sees the artist and must guess the hidden song title. 
Your task is to generate a helpful but not too obvious hint.

Instructions:
- Input: artist name and song title.
- Output: one hint in 2–3 sentences.
- The hint must never contain the exact song title.
- You can use:
  - a description of the song’s theme, mood, or emotions,
  - wordplay, metaphors, or indirect references to the title,
  - cultural or historical context,
  - information about the album, year, or genre.
- Do not quote lyrics directly.
- Mentioning the artist is allowed, but never reveal the song title itself.

Examples:
Input: "Queen - Bohemian Rhapsody"  
Output: "A groundbreaking track that blends ballad, rock, and opera into one dramatic piece. It became one of rock’s greatest anthems and redefined how long, complex songs could succeed on the radio."

Input: "Rick Astley - Never Gonna Give You Up"  
Output: "An upbeat 80s hit that later became one of the most famous internet memes. Today, it’s nearly impossible to hear it without thinking of a certain online prank."


Here is the artist and song title you need to generate a hint for: 
""";
}
