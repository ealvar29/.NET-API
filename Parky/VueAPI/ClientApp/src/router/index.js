import { createWebHistory, createRouter } from "vue-router";
import Home from "@/components/Home.vue";
import FetchParks from "@/components/FetchParks.vue";

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
        component: FetchParks,
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;