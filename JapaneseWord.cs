using System;

namespace oboete {
    public class JapaneseWord {
        public String romaji, kanji, meaning;

        public JapaneseWord(String romaji, String kanji, String meaning) {
            this.romaji = romaji;
            this.kanji = kanji;
            this.meaning = meaning;
        }
    }
}