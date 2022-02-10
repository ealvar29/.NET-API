import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home";
import FetchParks from "@/components/FetchParks";
import FetchTrails from "@/components/FetchTrails"

const routes = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
        path: "/FetchParks",
        name: "FetchParks",
        component: FetchParks,
    },
    {
        path: "/FetchTrails",
        name: "FetchTrails",
        component: FetchTrails,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;