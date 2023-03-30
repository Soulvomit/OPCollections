namespace FrequencyAnalysis
{
    public class FrequencyAnalyser
    {
        public static List<KeyValuePair<char, int>> AnalyzeFrequency(string encryptedText)
        {
            //create hash table to quickly store frequency of each letter
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            //for each character in encrypted text
            foreach (char c in encryptedText)
            {
                //if char is a letter
                if (Char.IsLetter(c))
                {
                    //if hash table already contains the character key
                    if (frequency.ContainsKey(c))
                        //increment frequency value
                        frequency[c]++;
                    else
                        //if hash table does not contain char key, add it with frequency of 1
                        frequency.Add(c, 1);
                }
            }
            //sort dictionary by frequency in descending order
            IOrderedEnumerable<KeyValuePair<char, int>> sortedFrequency 
                = frequency.OrderByDescending(x => x.Value);
            //return sorted frequency as a list
            return sortedFrequency.ToList();
        }
        public static string CaesarDecrypt(string encryptedText, int shiftKey)
        {
            //string to store decrypted result
            string decryptedText = "";
            //for each character in encrypted text
            foreach (char c in encryptedText)
            {
                //if character is not a letter
                if (!char.IsLetter(c))
                {
                    //add character to decryption text
                    decryptedText += c;
                }
                else
                {
                    //shift the character by shift key amount.
                    //26 = number of letters within the english alphabet.
                    //97 = starting index of lower case letters in ascii.
                    //modulo the result to 'wrap around' when char is not within the
                    //97-122 (lower case letters) ascii bound.
                    //this gives us a key result of [-26, 26], which is the valid key
                    //range for the caesar cipher within the english alphabet. 
                    int charValue = ((int)c - shiftKey - 97 + 26) % 26 + 97;
                    //add character to decryption text
                    decryptedText += (char)charValue;
                }
            }
            //return the decrypted text as a string
            return decryptedText;
        }
    }
}