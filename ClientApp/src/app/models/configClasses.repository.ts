export class Filter {
    category?: string;
    search?: string;
    related: boolean = false;

    reset() {
        this.category = this.search = "";
        this.related = false;
    }
}