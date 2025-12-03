import { TestBed } from '@angular/core/testing';

import { APProjectService } from './approject-service';

describe('APProjectService', () => {
  let service: APProjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(APProjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
