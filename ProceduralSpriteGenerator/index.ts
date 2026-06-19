import {exec} from 'child_process';
import * as fs from 'fs';
import * as path from 'path';

const outputFolder = path.resolve(__dirname, 'dist');
const newFilePath = path.join(outputFolder, 'new-file.txt');
if (!fs.existsSync(outputFolder)) {
    fs.mkdirSync(outputFolder, { recursive: true });
}
fs.writeFileSync(newFilePath, 'Hello from your automated workspace!');
console.log(`Successfully saved file to: ${newFilePath}`);

const librespritePath = '"D:\\Program Files\\libresprite-development-windows-x86_64\\libresprite.exe"';

exec(librespritePath, (error) => {
   if (error) {
        console.error(`Execution error: ${error.message}`);
        return;
    }
    console.log("Aseprite launched successfully!");
})