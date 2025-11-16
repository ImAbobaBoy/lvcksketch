const canvas = document.getElementById("c");
const ctx = canvas.getContext("2d");

let pings = [];
let lastPingPos = null;
const minDistance = 10; // минимальное расстояние для нового пинга

let isMouseDown = false;


const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/draw")
    .build();

hubConnection.on("ReceivePing", (point) => {
    pings.push({
        x: point.x,
        y: point.y,
        radius: 0,
        alpha: 1
    });
});

hubConnection.start()
    .then(() => console.log("SignalR connected"))
    .catch(err => console.error("SignalR connection error: ", err));


// ---- ФУНКЦИЯ СОЗДАНИЯ ПИНГА ----
function createPing(x, y) {
    pings.push({
        x,
        y,
        radius: 0,
        alpha: 1
    });
    lastPingPos = { x, y };

    hubConnection.invoke("SendPing", { x: x, y: y, color: "green" })
        .catch(err => console.error(err));
}

// ---- ПРОВЕРКА ДИСТАНЦИИ ----
function distance(p1, p2) {
    if (!p1 || !p2) return Infinity;
    const dx = p1.x - p2.x;
    const dy = p1.y - p2.y;
    return Math.sqrt(dx * dx + dy * dy);
}

// ---- СОБЫТИЯ ----

// ЛКМ нажата
canvas.addEventListener("mousedown", (e) => {
    if (e.button === 0) {
        isMouseDown = true;
        createPing(e.offsetX, e.offsetY);
    }
});

// ЛКМ отпущена
canvas.addEventListener("mouseup", (e) => {
    if (e.button === 0) {
        isMouseDown = false;
        lastPingPos = null; // сбрасываем, чтобы не "тянуть" расстояние
    }
});

// Мышь двигается
canvas.addEventListener("mousemove", (e) => {
    if (!isMouseDown) return; // условие — ЛКМ должна быть зажата

    const pos = { x: e.offsetX, y: e.offsetY };
    if (distance(pos, lastPingPos) >= minDistance) {
        createPing(pos.x, pos.y);
    }
});

// ---- АНИМАЦИЯ ----
function animate() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    for (let i = pings.length - 1; i >= 0; i--) {
        const p = pings[i];

        p.radius += 1.5;
        p.alpha -= 0.02;

        if (p.alpha <= 0) {
            pings.splice(i, 1);
            continue;
        }

        ctx.beginPath();
        ctx.arc(p.x, p.y, p.radius, 0, Math.PI * 2);
        ctx.strokeStyle = `rgba(0, 150, 255, ${p.alpha})`;
        ctx.lineWidth = 2;
        ctx.stroke();
    }

    requestAnimationFrame(animate);
}

animate();
