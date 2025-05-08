import expres, { Request, Response } from "express";
import bodyParser from "body-parser";
import { chromium, devices } from "playwright";

const app = expres();
const port = 3000;

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(bodyParser.raw());

app.post("/", async (request: Request, response: Response) => {
  const browser = await chromium.launch();
  const context = await browser.newContext(devices["Desktop Chrome"]);
  const page = await context.newPage();

  console.log(request.body);
  await page.goto(request.body["url"]);

  const images: string[] = [];
  const imageElements = await page.getByRole("img").all();

  for (const image of imageElements) {
    const imageSrc = await image.getAttribute("src");

    if (!imageSrc) continue;

    images.push(imageSrc!);
  }

  response.send(images);
});

app.listen(port, () => {
  console.log("app listening on port 3000");
});
